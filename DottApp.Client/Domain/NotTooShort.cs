using System;
using System.Globalization;
using System.Windows.Controls;

namespace DottApp.Client.Domain
{
    class NotTooShort : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return (value ?? "").ToString().Length < 6
                ? new ValidationResult(false, "Minimum 6 characters")
                : ValidationResult.ValidResult;
        }
    }
}
