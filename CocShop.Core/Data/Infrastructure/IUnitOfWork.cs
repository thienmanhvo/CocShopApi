using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
