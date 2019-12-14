using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Repository.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }

        public async Task<ICollection<Store>> GetAllNearestStore(double latpoint, double longpoint, double radius, int? offset = null, int? limit = null)
        {
            var p1 = new SqlParameter("@latpoint", latpoint);
            var p2 = new SqlParameter("@longpoint", longpoint);
            var p3 = new SqlParameter("@radius", radius);
            var p4 = new SqlParameter("@offset", offset);
            var p5 = new SqlParameter("@limit", limit);

            string sql = $@"SELECT
                                g.*, 
                                (SELECT Count(*) FROM dbo.Store s WHERE s.Brand_Id = g.Brand_Id) AS Total_Store
                                
                            FROM(
                                select f.*,
                                		ROW_NUMBER() OVER(Partition by f.Brand_Id ORDER BY f.distance) AS Row
                                from (SELECT *
                                	  
                                  FROM (
                                 SELECT z.*,
                                        p.radius,
                                        p.distance_unit
                                                 * DEGREES(ACOS(COS(RADIANS(p.latpoint))
                                                 * COS(RADIANS(z.latitude))
                                                 * COS(RADIANS(p.longpoint - z.longitude))
                                                 + SIN(RADIANS(p.latpoint))
                                                 * SIN(RADIANS(z.latitude)))) AS distance
                                  FROM Store AS z
                                  JOIN (   /* these are the query parameters */
                                        SELECT  @latpoint  AS latpoint,  @longpoint AS longpoint,
                                                @radius AS radius,      111.045 AS distance_unit
                                    ) AS p ON 1=1
                                  WHERE z.latitude
                                     BETWEEN p.latpoint  - (p.radius / p.distance_unit)
                                         AND p.latpoint  + (p.radius / p.distance_unit)
                                    AND z.longitude
                                     BETWEEN p.longpoint - (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
                                         AND p.longpoint + (p.radius / (p.distance_unit * COS(RADIANS(p.latpoint))))
                                 ) AS d
                                 WHERE distance <= radius) f) g 
                             WHERE g.Row = 1 
                             ORDER BY g.distance
                             OFFSET @offset ROWS 
                             FETCH NEXT @limit ROWS ONLY";
            return await DbContext.Store.FromSql(sql, p1, p2, p3,p4,p5).ToListAsync();
        }
    }
}
