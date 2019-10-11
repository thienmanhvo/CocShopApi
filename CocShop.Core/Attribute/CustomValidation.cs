using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CocShop.Core.Attribute
{
    public class CustomValidation
    {
        public sealed class CheckGuid : ValidationAttribute
        {
            public string Property { get; set; }
            protected override ValidationResult IsValid(object id, ValidationContext validationContext)
            {
                if (!Guid.TryParse(id as string, out Guid guidId))
                {
                    return new ValidationResult($"Invalid {Property}");
                }

                return ValidationResult.Success;
            }
        }
    }
}
