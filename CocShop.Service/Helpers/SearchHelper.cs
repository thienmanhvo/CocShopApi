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
        public static string GenerateStringExpression(IDictionary<string, string> searchRange, string defaultCondition = null)
        {
            string search = "";
            string result = "";
            string lamda = "_ => ";
            string searchRangeResult = "";
            defaultCondition = string.IsNullOrEmpty(defaultCondition) ? "" : defaultCondition + " && ";
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
                                throw new Exception("Operation not support");
                                //continue;
                        }
                        if (field.PropertyType.Equals(typeof(string)))
                        {
                            if (opera.Equals("="))
                            {
                                var value = item.Value?.ToLower();
                                search += $"|| _.{field.Name}.ToLower().Contains(\"{value}\")";
                            }
                            else
                            {
                                throw new Exception("Only support Equal operation for string");
                            }
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
                                        throw new Exception("Value of boolean type is Invalid");
                                        //break;
                                }
                            }
                            else
                            {
                                throw new Exception("Only support Equal operation for boolean");
                            }
                        }
                        else if (field.PropertyType.Equals(typeof(DateTime?)) || field.PropertyType.Equals(typeof(DateTime)))
                        {
                            var date = DateTime.ParseExact(item.Value, "yyyyMMdd", CultureInfo.InvariantCulture);
                            searchRangeResult += $"&&  _.{field.Name} {opera} System.DateTime.ParseExact(\"{item.Value}\", \"yyyyMMdd\",  System.Globalization.CultureInfo.InvariantCulture)";
                        }
                        else if (field.PropertyType.Equals(typeof(Guid?)) || field.PropertyType.Equals(typeof(Guid)))
                        {
                            if (opera.Equals("="))
                            {
                                search += $"|| _.{field.Name} == new System.Guid(\"{item.Value}\")";
                            }
                            else
                            {
                                throw new Exception("Only support Equal operation for Guid");
                            }
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
                    result = lamda + defaultCondition.Substring(0, defaultCondition.Length - 4);
                }
                else
                {
                    if (!string.IsNullOrEmpty(search) && !string.IsNullOrEmpty(searchRangeResult))
                    {
                        result = $"{lamda} {defaultCondition} ({search}) && ({searchRangeResult})";
                    }
                    else if (!string.IsNullOrEmpty(search))
                    {
                        result = $"{lamda} {defaultCondition} ({search})";
                    }
                    else
                    {
                        result = $"{lamda} {defaultCondition} ({searchRangeResult})";
                    }
                }
            }
            else
            {
                result = string.IsNullOrEmpty(defaultCondition) ? "" : lamda + defaultCondition.Substring(0, defaultCondition.Length - 4);
            }
            return result;
        }
    }
}
