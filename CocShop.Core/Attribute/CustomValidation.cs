using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CocShop.Core.Attribute
{
    public class CheckGuidAttribute : ValidationAttribute
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

    public class CheckDateAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                try
                {
                    DateTime.ParseExact(value as string, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return new ValidationResult($"Invalid Date (Valid: yyyyMMdd)");
                }

            }
            return ValidationResult.Success;
        }
    }
    public class CheckGuidOrNullAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object id, ValidationContext validationContext)
        {
            if (id != null)
            {
                if (!Guid.TryParse(id as string, out Guid guidId))
                {
                    return new ValidationResult($"Invalid {Property}");
                }
            }
            return ValidationResult.Success;
        }
    }
    public class CheckUrlAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object uriName, ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(uriName as string))
                return Uri.TryCreate(uriName as string, UriKind.Absolute, out Uri uriResult)
                     && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps) == true
                     ? ValidationResult.Success
                     : new ValidationResult($"Invalid URL");
            return ValidationResult.Success;
        }
    }
    public class CheckNameAttribute : ValidationAttribute
    {
        public string Property { get; set; }
        protected override ValidationResult IsValid(object inputText, ValidationContext validationContext)
        {
            //@"^(\\b[A-Za-z]*\\b\\s+\\b[A-Za-z]*\\b+\\.[A-Za-z])$",
            Regex regex = new Regex(
                                       "^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹ\\s]+$",
                                   RegexOptions.IgnoreCase
                                   | RegexOptions.CultureInvariant
                                   | RegexOptions.IgnorePatternWhitespace
                                   | RegexOptions.Compiled
                                   );


            if (regex.IsMatch(inputText as string))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"{Property} can't have number and special character");
        }
    }
    public class CheckCard : ValidationAttribute
    {
        protected override ValidationResult IsValid(object card, ValidationContext validationContext)
        {
            var carNumver = NormalizeCardNumber(card?.ToString() ?? "");

            return IsCardNumberValid(carNumver) ? ValidationResult.Success : new ValidationResult($"Invalid card");
        }
        public bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;

            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');

            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }

            // Number is valid if sum of both checksums MOD 10 equals 0
            return ((checkSum % 10) == 0);
        }
        public string NormalizeCardNumber(string cardNumber)
        {
            if (cardNumber == null)
                cardNumber = String.Empty;

            StringBuilder sb = new StringBuilder();

            foreach (char c in cardNumber)
            {
                if (Char.IsDigit(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }
    }

}
