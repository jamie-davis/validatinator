using System;
using System.Linq;
using FluentAssertions;
using Validatinator;
using Validatinator.Attributes;
using Validatinator.Discovery;
using Validatinator.Validation;
using ValidatinatorTests.Validation.TestTypes;
using Xunit;

namespace ValidatinatorTests.Validation
{
    public class TestValidationRuleChecker
    {
        private IValidationCatalog _validations;

        public TestValidationRuleChecker()
        {
            var validationCatalog = new ValidationCatalog();
            _validations = validationCatalog;
            validationCatalog.AddValidations(ValidationFinder.ScanAssembly(GetType().Assembly));
        }

        #region Types for test

        [ValidationEntity("Customer")]
        class BadCustomer
        {
            public DateTime LastName { get; set; }
        }

        #endregion

        [Fact]
        public void FieldTypeMismatchExcludesValidation()
        {
            //Arrange
            var set = new TargetSet();
            set.Add(new BadCustomer());
            var validation = _validations.Validations()
                .Single(v => v.ContainingType == typeof(CustomerSimple) && v.Method.Name == nameof(CustomerSimple.SurnameIsMandatory));
            var candidates = CandidateSelector.AllPossible(validation, set);
            
            //Act
            var error = ValidationRuleChecker.Check(candidates[0]).Single();

            //Assert
            error.Message.Should().Contain("incorrect type");
        }
    }

}
