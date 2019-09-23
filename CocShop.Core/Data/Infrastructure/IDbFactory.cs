using CocShop.Core.Data.Entity;
using System;

namespace CocShop.Core.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DataContext Init();
    }
}
