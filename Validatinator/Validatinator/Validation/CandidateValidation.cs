using System.Collections.Generic;
using System.Linq;
using Validatinator.Discovery;

namespace Validatinator.Validation
{
    internal class CandidateValidation
    {
        public ValidationInfo Validation { get; }

        public CandidateValidation(ValidationInfo validation, ValidationFieldSource extraFieldSource)
        {
            Validation = validation;
            Fields = new List<ValidationFieldSource> {extraFieldSource};
        }

        public CandidateValidation(ValidationFieldSource extraFieldSource, CandidateValidation candidateValidation)
        {
            Validation = candidateValidation.Validation;
            Fields = candidateValidation.Fields.Concat(Enumerate(extraFieldSource)).ToList();
        }

        private IEnumerable<ValidationFieldSource> Enumerate(ValidationFieldSource extraFieldSource)
        {
            yield return extraFieldSource;
        }

        internal List<ValidationFieldSource> Fields { get; private set; }
    }
}