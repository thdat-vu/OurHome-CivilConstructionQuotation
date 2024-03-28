using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Model.ValidationModels
{
    public class MustBeIntegerAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return ValidationResult.Success;

            int result;
            if (!int.TryParse(value.ToString(), out result))
            {
                return new ValidationResult(ErrorMessage ?? "{0} phải là một số nguyên", new[] { validationContext.DisplayName });
            }

            return ValidationResult.Success;
        }
    }
}
