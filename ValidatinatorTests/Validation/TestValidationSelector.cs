using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator.Discovery;
using Validatinator.Validation;
using ValidatinatorTests.Validation.TestTypes;
using Xunit;

namespace ValidatinatorTests.Validation
{
    public class TestValidationSelector
    {

        [Fact]
        public void NoRulesValidationsAreSelected()
        {
            //Arrange
            var catalog = new ValidationCatalog();
            catalog.AddValidations(ValidationFinder.ScanAssembly(GetType().Assembly, t => t == typeof(CustomerSimple)));

            var targetSet = new TargetSet();
            targetSet.Add(new CustomerInstance());

            //Act
            var selected = ValidationSelector.Select(catalog, targetSet);

            //Assert
            var output = new Output();
            output.FormatTable(selected.Select(s => new
            {
                Method = s.Validation.Method.Name, 
                Inputs = string.Join(", ", s.Targets.Select(t => t.GetType().Name))
            }));

            output.Report.Verify();
        }
    }
}
