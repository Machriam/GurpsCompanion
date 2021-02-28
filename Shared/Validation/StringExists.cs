using System.ComponentModel.DataAnnotations;

namespace GurpsCompanion.Shared.Validation
{
    public class StringExists : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return !string.IsNullOrWhiteSpace(value?.ToString());
        }
    }
}
