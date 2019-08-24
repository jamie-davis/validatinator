using System.Collections.Generic;

namespace Validatinator.RuleParsing
{
    internal abstract class ApplyIfRuleClause
    {
        internal abstract string Describe();

        internal abstract bool GetRequireFirstError();
        internal abstract bool GetRequireCleanInput();
        internal abstract IEnumerable<EntityReference> GetMatches();
    }
}