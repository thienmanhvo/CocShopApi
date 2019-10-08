using System;
using System.Linq;
using System.Linq.Expressions;

namespace CocShop.Core.Data.Query
{
    public class QueryArgs<T> where T : class
    {
        //public string Predicate { get; set; }
        //public object[] PredicateParameters { get; set; }
        //public string Order { get; set; }
        //public int Page { get; set; }
        //public int Limit { get; set; }
        public Expression<Func<T, bool>> Filter { get; set; }
        public string Sort { get; set; }
        public int? Offset { get; set; }
        public int? Limit { get; set; }
    }
}
