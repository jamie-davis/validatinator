using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using TestConsoleLib;
using TestConsoleLib.Testing;
using Validatinator.Attributes;
using Validatinator.Validation;
using Xunit;

namespace ValidatinatorTests.Validation
{
    public class TestTargetAnalyser
    {
        #region Types for test

        [ValidationEntity("Customer")]
        class Customer
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        [ValidationEntity("Customer")]
        class Customer2
        {
            public int Id { get; set; }
            public string FirstName { get; set; }

            [ValidationField("Customer.LastName")]
            public string Surname { get; set; }
        }

        [ValidationEntity("Customer")]
        class Customer3
        {
            public int Id { get; set; }
            public string FirstName { get; set; }

            [NotValidated()]
            public string LastName { get; set; }
        }

        #endregion

        [Fact]
        public void EntityNameIsExtractedFromClass()
        {
            //Arrange
            var item = new Customer();

            //Act
            var result = TargetAnalyser.Analyse(item);

            //Assert
            result.Entity.Should().Be("Customer");
        }

        [Fact]
        public void EntityFieldsAreExtractedFromClass()
        {
            //Arrange
            var item = new Customer();

            //Act
            var result = TargetAnalyser.Analyse(item);

            //Assert
            var output = new Output();
            output.FormatTable(result.Fields.Select(f => new { f.Entity, Field = f.Field.Describe(), Type = f.Type.Name}));
            output.Report.Verify();
        }

        [Fact]
        public void EntityNamesCanBeOverridden()
        {
            //Arrange
            var item = new Customer2();

            //Act
            var result = TargetAnalyser.Analyse(item);

            //Assert
            var output = new Output();
            output.FormatTable(result.Fields.Select(f => new { f.Entity, Field = f.Field.Describe(), Type = f.Type.Name}));
            output.Report.Verify();
        }

        [Fact]
        public void FieldsCanBeHiddenFromValidation()
        {
            //Arrange
            var item = new Customer3();

            //Act
            var result = TargetAnalyser.Analyse(item);

            //Assert
            var output = new Output();
            output.FormatTable(result.Fields.Select(f => new { f.Entity, Field = f.Field.Describe(), Type = f.Type.Name}));
            output.Report.Verify();
        }
    }
}
