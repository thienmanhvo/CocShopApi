using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Constaint
{
    public class Constants
    {
        public const string USER_ANONYMOUS = "anonymous";
        public const string CLAIM_USERNAME = "username";
        public const int MAX_PAGE_SIZE = 50;
        public const int DEFAULT_PAGE_SIZE = 10;
        public const int DEFAULT_PAGE_INDEX = 1;
        public const string DEAFAULT_SORT_FIELD = "CreatedAt";
        public const string DEAFAULT_SORT_BY = "ASC";
        public const string SORT_BY_ASC = "ASC";
        public const string SORT_BY_DESC = "DESC";
        public const string DEAFAULT_DELETE_STATUS_EXPRESSION = "_ => _.IsDelete == false";
        public const string GREATER_THAN = "gt";
        public const string GREATER_THAN_EQUAL = "gte";
        public const string LESSTER_THAN = "lt";
        public const string LESSTER_THAN_EQUAL = "lte";
        public const string EQUAL = "e";
        
        
    }
    public class ErrMessageConstants
    {
        public const string NOTFOUND = "Not_Found";
        public const string INVALID_PASSWORD = "Invalid_Password";
        public const string INVALID_USERNAME = "Invalid_Username";
        public const string FAILURE = "Failure";
        public const string PRODUCT_NOT_FOUND = "Product_Not_Found";
        public const string PRODUCT_PRICE_NOT_FOUND = "Product_Price_Not_Found";

    }
    public class MessageConstants
    {
        public const string SUCCESS = "Success";
        public const string NO_RECORD = "No_Record";

    }
}
