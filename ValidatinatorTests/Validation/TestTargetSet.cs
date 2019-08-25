using System.Linq;
using FluentAssertions;
using Validatinator.Attributes;
using Validatinator.RuleParsing;
using Validatinator.Validation;
using Xunit;

namespace ValidatinatorTests.Validation
{
    public class TestTargetSet
    {
        #region Types For Test

        [ValidationEntity("Customer")]
        class Customer
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        #endregion

        [Fact]
        public void EntitiesAreKnownToSet()
        {
            //Arrange
            var set = new TargetSet();
            var customer = new Customer();

            //Act
            set.Add(customer);
            
            //Assert
            set.Contains(customer);
        }

        [Fact]
        public void EntitiesAreRemovedFromSet()
        {
            //Arrange
            var set = new TargetSet();
            var customer = new Customer();
            set.Add(customer);

            //Act
            set.Remove(customer);
            
            //Assert
            set.Contains(customer).Should().BeFalse();
        }

        [Fact]
        public void EntityFieldsAreReturned()
        {
            //Arrange
            var set = new TargetSet();
            var customer = new Customer();
            set.Add(customer);
            var idRef = new EntityReference("Customer", "Id");

            //Act
            var result = set.GetFields(idRef);
            
            //Assert
            result.Any(f => ReferenceEquals(f.Target, customer)).Should().BeTrue();
        }

        [Fact]
        public void EntityFieldsAreForgotten()
        {
            //Arrange
            var set = new TargetSet();
            var customer = new Customer();
            set.Add(customer);
            var idRef = new EntityReference("Customer", "Id");

            set.Remove(customer);

            //Act
            var result = set.GetFields(idRef);
            
            //Assert
            result.Any(f => ReferenceEquals(f.Target, customer)).Should().BeFalse();
        }
    }
}
