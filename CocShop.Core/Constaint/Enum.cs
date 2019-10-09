using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
            //"Quản lý tài khoản"
            Admin = 0,
            //"Nhân viên"
            Staff = 1,
            //"Khách Hàng")
            User = 2,
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Order
        {
            Asc = 0,
            Desc = 1,
        }
        [JsonConverter(typeof(StringEnumConverter))]
        public enum OrderStatus
        {
            Submitted = 0,
            Delivered = 1,
            Canceled = 2,
        }

    }
}
