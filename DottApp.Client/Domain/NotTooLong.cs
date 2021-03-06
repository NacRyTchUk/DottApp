using System;
using System.Globalization;
using System.Windows.Controls;
using DottApp.Client.ViewModels;

namespace DottApp.Client.Domain
{
    class NotTooLong : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return (value ?? "").ToString().Length > 35
                ? new ValidationResult(false, "Maximum 35 characters")
                : ValidationResult.ValidResult;
        }
    }
}
