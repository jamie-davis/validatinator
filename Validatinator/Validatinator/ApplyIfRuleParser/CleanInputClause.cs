using System.Collections.Generic;

namespace Validatinator.ApplyIfRuleParser
{
    internal class CleanInputClause : ApplyIfRuleClause
    {
        #region Overrides of ApplyIfRuleClause

        internal override string Describe()
        {
            return "CleanInput";
        }

        internal override bool GetRequireFirstError()
        {
            return false;
        }

        internal override bool GetRequireCleanInput()
        {
            return true;
        }

        internal override IEnumerable<EntityReference> GetMatches()
        {
            yield break;
        }
        #endregion
    }
}