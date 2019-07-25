using System.Diagnostics;
using System.Linq;
using FluentAssertions;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator;
using Validatinator.Attributes;
using Validatinator.Discovery;
using ValidatinatorTests.Discovery.TestValidations;
using Xunit;

namespace ValidatinatorTests.Discovery
{
    namespace TestValidations
    {
        [Validation]
        public static class StringValidation
        {
            public static ValidationResult SurnameIsMandatory([Field("Customer.Surname")] string surname)
            {
                return ValidationResult.Valid;
            }

            public static ValidationResult FirstNameIsMandatory([Field("Customer.FirstName")] string firstname)
            {
                return ValidationResult.Valid;
            }

            public static string NotAValidator(string firstName, string surname)
            {
                return $"{surname}, {firstName}";
            }
        }
    }

    namespace FilteredOutValidations
    {
        [Validation]
        public static class MoreStringValidation
        {
            public static ValidationResult SurnameIsMandatory([Field("Customer.Surname")] string surname)
            {
                return ValidationResult.Valid;
            }
        }
    }

    public class TestValidationFinder
    {
        private readonly string _testNamespace;

        public TestValidationFinder()
        {
            _testNamespace = typeof(StringValidation).Namespace;
            Debug.Assert(!string.IsNullOrEmpty(_testNamespace));
        }

        [Fact]
        public void ValidationsAreRecognised()
        {
            //Arrange
            var assembly = GetType().Assembly;

            //Act
            var validations = ValidationFinder.ScanAssembly(assembly, t => t?.Namespace == _testNamespace);

            //Assert
            var output = new Output();
            output.FormatTable(validations.Select(v => new {ContainingType = v.ContainingType.Name,  Method = v.Method.Name}));
            output.Report.Verify();
        }

        [Fact]
        public void FilterFunctionIsOptional()
        {
            //Arrange
            var assembly = GetType().Assembly;
            var filteredValidationCount = ValidationFinder.ScanAssembly(assembly, t => t?.Namespace == _testNamespace).Count();

            //Act
            var unfilteredValidationCount = ValidationFinder.ScanAssembly(assembly).Count();

            //Assert
            unfilteredValidationCount.Should().BeGreaterThan(filteredValidationCount);
        }

        [Fact]
        public void FilterFunctionIsCalled()
        {
            //Arrange
            var assembly = GetType().Assembly;

            //Act
            var filteredValidationCount = ValidationFinder.ScanAssembly(assembly, t => false).Count();

            //Assert
            filteredValidationCount.Should().Be(0);
        }
    }
}