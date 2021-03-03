using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GurpsCompanion.Shared.Core
{
    public interface IObjectValidator
    {
        bool ModelIsValid(object model, out List<ValidationResult> validationResults);
    }

    public class ObjectValidator : IObjectValidator
    {
        public bool ModelIsValid(object model, out List<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(model);
            validationResults = new List<ValidationResult>();
            if (model != null) return Validator.TryValidateObject(model, validationContext, validationResults, true);
            validationResults.Add(new ValidationResult("Object was null"));
            return false;
        }
    }
}
