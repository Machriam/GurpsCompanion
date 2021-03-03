using System.ComponentModel.DataAnnotations;
using GurpsCompanion.Shared.Extensions;

namespace GurpsCompanion.Shared.Validation
{
    public class AdvantageLevelCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var skillable = context.GetValue<bool>("Skillable");
            var level = (long)value;
            if ((level == 1 && !skillable) || (skillable && level >= 1)) return ValidationResult.Success;
            return new ValidationResult(ErrorMessage);
        }
    }
}
