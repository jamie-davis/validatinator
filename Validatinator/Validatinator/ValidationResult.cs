using System;
using System.Collections.Generic;
using System.Text;

namespace Validatinator
{
    public class ValidationResult
    {
        private ValidationResult()
        {
        }

        public static ValidationResult Valid { get; } = new ValidationResult();

        public static ValidationResult Error(string message)
        {
            throw new NotImplementedException();
        }
    }
}
