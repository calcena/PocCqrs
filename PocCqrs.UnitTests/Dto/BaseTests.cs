
namespace PocCqrs.Test.UnitTest.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public abstract class BaseTests
    {
        public IList<ValidationResult> ValidateModel (object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
