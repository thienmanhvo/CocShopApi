using CocShop.Core.Constaint;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CocShop.Service.Helpers
{
    public static class SearchHelper<T> where T : class
    {
        public static string GenerateStringExpression(IDictionary<string, string> searchRange)
        {
            string search = "";
            var deleteStatus = Constants.DEAFAULT_DELETE_STATUS_EXPRESSION;
            string result = "";
            string searchRangeResult = "";
            if (searchRange?.Count > 0)
            {

                foreach (var item in searchRange)
                {
                    var prop = new string(item.Key.TakeWhile(_ => _ != '(').ToArray());
                    var field = typeof(T).GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (field != null)
                    {
                        var opera = new string(item.Key.SkipWhile(_ => _ != '(').ToArray()).Replace(")", "").Replace("(", "")?.ToLower();
                        switch (opera)
                        {
                            case Constants.GREATER_THAN:
                                opera = ">";
                                break;
                            case Constants.GREATER_THAN_EQUAL:
                                opera = ">=";
                                break;
                            case Constants.LESSTER_THAN:
                                opera = "<";
                                break;
                            case Constants.LESSTER_THAN_EQUAL:
                                opera = "<=";
                                break;
                            case Constants.EQUAL:
                                opera = "=";
                                break;

                            default:
                                continue;
                        }
                        if (field.PropertyType.Equals(typeof(string)))
                        {
                            if (opera.Equals("="))
                            {
                                var value = item.Value.ToLower();
                                search += $"|| _.{field.Name}.ToLower().Contains(\"{value}\")";
                            }
                            continue;
                        }
                        else if (field.PropertyType.Equals(typeof(bool?)) || field.PropertyType.Equals(typeof(bool)))
                        {
                            if (opera.Equals("="))
                            {
                                var value = item.Value.ToLower();
                                switch (value.ToLower())
                                {
                                    case "1":
                                        search += $"|| _.{field.Name} == true";
                                        break;
                                    case "true":
                                        search += $"|| _.{field.Name} == true";
                                        break;
                                    case "0":
                                        search += $"|| _.{field.Name} == false";
                                        break;
                                    case "false":
                                        search += $"|| _.{field.Name} == false";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            continue;
                        }
                        else if (field.PropertyType.Equals(typeof(DateTime?)) || field.PropertyType.Equals(typeof(DateTime)))
                        {
                            var date = DateTime.ParseExact(item.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                            searchRangeResult += $"&&  _.{field.Name} {opera} System.DateTime.ParseExact(\"{item.Value}\", \"yyyyMMdd\",  System.Globalization.CultureInfo.InvariantCulture)";
                        }
                        else
                        {
                            if (opera.Equals("="))
                            {
                                //bool isNum = true;
                                foreach (var num in item.Value.ToArray())
                                {
                                    if (!Char.IsNumber(num))
                                    {
                                        //isNum = false;
                                        //break;
                                        throw new Exception($"{item.Value} is not a number.");
                                    }
                                }
                                // search += isNum ? $"|| _.{field.Name} == {item.Value}" : "|| 1 != 1";
                                search += $"|| _.{field.Name} == {item.Value}";
                            }
                            else
                            {
                                searchRangeResult += $"&& _.{field.Name} {opera} {item.Value}";
                            }
                        }
                    }
                }
                if (!searchRangeResult.Equals(""))
                {
                    searchRangeResult = searchRangeResult.Substring(3);
                }
                if (!search.Equals(""))
                {
                    search = search.Substring(3);
                }

                if (string.IsNullOrEmpty(search + searchRangeResult))
                {
                    result = deleteStatus;
                }
                else
                {
                    if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(searchRangeResult))
                    {
                        result = $"{deleteStatus} && ({search}) && ({searchRangeResult})";
                    }
                    else if (!string.IsNullOrEmpty(search))
                    {
                        result = $"{deleteStatus} && ({search})";
                    }
                    else
                    {
                        result = $"{deleteStatus}  && ({searchRangeResult})";
                    }
                }
            }
            else
            {
                result = deleteStatus;
            }
            return result;
        }
    }
}
