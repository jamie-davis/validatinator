using Validatinator;
using Validatinator.Attributes;
// ReSharper disable UnusedMember.Global

namespace ValidatinatorTests.Validation.TestTypes
{
    [Validation]
    public static class CustomerSimple
    {
        public static ValidationResult SurnameIsMandatory([Field("Customer.LastName")] string lastName)
        {
            if (string.IsNullOrEmpty(lastName))
                return ValidationResult.Error("Last name must be specified.");

            return ValidationResult.Valid;
        }
    }
}
