using CocShop.Core.Constaint;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Exceptions
{
    public class ErrorException : Exception
    {
        public int ErrorCode { get; }

        public ErrorException(string message) : base(message)
        {
            ErrorCode = (int)MyEnum.ErrorCode.UnknownError;
        }

        public ErrorException(int errorCode, string message) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
