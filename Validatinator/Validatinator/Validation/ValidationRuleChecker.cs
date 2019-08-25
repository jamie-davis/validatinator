using System;
using System.Collections.Generic;

namespace Validatinator.Validation
{
    internal static class ValidationRuleChecker
    {
        public static IEnumerable<RequiredValidation> Filter(List<CandidateValidation> allPossible)
        {
            foreach (var candidateValidation in allPossible)
            {
                yield return new RequiredValidation(candidateValidation);
            }
        }
    }
}