using System.Collections;
using System.Collections.Generic;

namespace Validatinator.ApplyIfRuleParser
{
    internal abstract class ApplyIfRuleClause
    {
        internal abstract string Describe();

        internal abstract bool GetRequireFirstError();
        internal abstract bool GetRequireCleanInput();
        internal abstract IEnumerable<EntityReference> GetMatches();
    }
}