using System.Collections.Generic;
using System.Linq;
using Validatinator.Discovery;

namespace Validatinator.Validation
{
    internal static class CandidateSelector
    {
        internal static List<CandidateValidation> AllPossible(ValidationInfo val, TargetSet targetSet)
        {
            List<CandidateValidation> set = null;
            foreach (var fieldInfo in val.Fields)
            {
                var fieldTargets = targetSet.GetFields(fieldInfo.Name).Select(f => new ValidationFieldSource(f.Target, fieldInfo, f));
                if (set == null)
                    set = fieldTargets.Select(f => new CandidateValidation(val, f) ).ToList();
                else
                    set = fieldTargets.SelectMany(t => set.Select(s => new CandidateValidation(t, s))).ToList();
            }

            return set;
        }
    }
}