using System;
using System.Collections.Generic;
using System.Text;

namespace CocShop.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public ICollection<KeyValuePair<string, ICollection<string>>> Errors { get; }

        public BadRequestException(ICollection<KeyValuePair<string, ICollection<string>>> errors)
        {
            Errors = errors;
        }

        public BadRequestException(string key, string[] errors)
        {
            Errors = new[]
            {
                new KeyValuePair<string, ICollection<string>>(key, errors)
            };
        }
    }
}
