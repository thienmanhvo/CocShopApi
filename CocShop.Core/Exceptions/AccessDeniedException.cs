using CocShop.Core.Constaint;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public int ErrorCode { get; }

        public AccessDeniedException(string message) : base(message)
        {
            ErrorCode = (int)MyEnum.ErrorCode.AccessDenied;
        }

        public AccessDeniedException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
