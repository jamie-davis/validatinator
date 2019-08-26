using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validatinator.Validation
{
    internal static class ValidationSelector
    {
        internal static IEnumerable<RequiredValidation> Select(IValidationCatalog catalog, TargetSet targetSet)
        {
            var allPossible = catalog.Validations().SelectMany(v => CandidateSelector.AllPossible(v, targetSet)).ToList();
            return ValidationRuleChecker.Filter(allPossible);
        }
    }
}
