using System;
using System.Globalization;
using System.Windows.Controls;
using DottApp.Client.ViewModels;

namespace DottApp.Client.Domain
{
    class EqualPasswords : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            System.Windows.Media.Brush brush = value as System.Windows.Media.Brush;
            if (brush != null) 
                return brush == System.Windows.Media.Brushes.Red
                ? new ValidationResult(false, "Password mismatch")
                : ValidationResult.ValidResult;
            else return new ValidationResult(false, "Password mismatch");
        }
    }
}
