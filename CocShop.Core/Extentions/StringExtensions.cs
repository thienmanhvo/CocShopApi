using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Extentions
{
    public static class StringExtensions
    {
        public static string ConvertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static EnumType ConverToEnum<EnumType>(this string enumValue)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
        }
    }
}
