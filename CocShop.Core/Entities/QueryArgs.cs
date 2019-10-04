using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GICBC.Common
{
    public class QueryArgs
    {
        public string Predicate { get; set; }
        public object[] PredicateParameters { get; set; }
        public string Order { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
      
    }
}
