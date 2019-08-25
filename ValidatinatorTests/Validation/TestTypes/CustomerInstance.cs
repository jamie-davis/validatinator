using Validatinator.Attributes;

namespace ValidatinatorTests.Validation.TestTypes
{
    [ValidationEntity("Customer")]
    public class CustomerInstance
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}