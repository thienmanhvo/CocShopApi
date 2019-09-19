using CocShop.Data.Entity;
using System;

namespace CocShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DataContext Init();
    }
}
