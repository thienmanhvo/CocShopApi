using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CocShop.Service.Helpers
{
    public static class IncludeLinqHelper<T> where T : class
    {
        public static IEnumerable<string> StringToListInclude(string include)
        {
            IList<string> includeList = new List<string>();

            foreach (var includeProperty in (include ?? "").Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var field = typeof(T).GetProperty(includeProperty, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (!string.IsNullOrEmpty(field?.Name))
                {
                    includeList.Add(field.Name);
                }
            }
            return includeList;
        }

    }
}
