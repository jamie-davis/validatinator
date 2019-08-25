using System.Collections.Generic;
using System.Linq;
using Validatinator.Discovery;

namespace Validatinator.Validation
{
    internal class RequiredValidation
    {
        public ValidationInfo Validation { get; }

        public RequiredValidation(CandidateValidation candidateValidation)
        {
            Validation = candidateValidation.Validation;

            Targets = candidateValidation.Fields.Select(f => f.Target).Distinct().ToList();
        }

        public IEnumerable<object> Targets { get; }
    }
}