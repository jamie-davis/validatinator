using FluentAssertions;
using Validatinator.Validation;
using Xunit;

namespace ValidatinatorTests.Validation
{
    public class TestTypeCompatibility
    {
        [Fact]
        public void MatchingTypesAreCompatible()
        {
            //Act/Assert
            TypeCompatibility.Check(typeof(string), typeof(string)).Should().BeTrue();
        }
    }
}
