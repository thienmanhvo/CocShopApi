using CocShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        DataContext Init();
    }
}
