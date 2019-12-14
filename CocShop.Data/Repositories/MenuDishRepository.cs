using CocShop.Core.Data.Entity;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Repository.Repositories
{
    public class MenuDishRepository : RepositoryBase<MenuDish>, IMenuDishRepository
    {
        public MenuDishRepository(IDbFactory dbFactory, IServiceProvider serviceProvider) : base(dbFactory, serviceProvider)
        {
        }
    }
}
