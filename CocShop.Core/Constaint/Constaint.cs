using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Constaint
{
    public class Constants
    {
        public const string USER_ANONYMOUS = "anonymous";
        public const string CLAIM_USERNAME = "username";
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int DEFAULT_PAGE_INDEX = 0;
    }
    public class ErrMessageConstants
    {
        public const string NOTFOUND = "NotFound";
        public const string INVALID_PASSWORD = "InvalidPassword";
        public const string INVALID_USERNAME = "InvalidUsername";
        public const string FAILURE = "Failure";

    }
    public class MessageConstants
    {
        public const string SUCCESS = "Success";
        public const string NO_RECORD = "NoRecord";

    }
}
