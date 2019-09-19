using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Constaint
{
    public class MyEnum
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ErrorCode
        {
            UnknownError = -99,
            EntityNotFound = -1,
            BadRequest = -2,
            IncorrectUsername = -3,
            IncorrectPassword = -4,
            UserLockedOut = -5,
            AccessDenied = -6,
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Role
        {
            Admin = 1,
            User = 2
        }

    }
}
