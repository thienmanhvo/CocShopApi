using CocShop.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        DataContext dbContext;

        public DataContext Init()
        {
            return dbContext ?? (dbContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
