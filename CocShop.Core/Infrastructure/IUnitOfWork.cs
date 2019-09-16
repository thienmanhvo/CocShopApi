using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
