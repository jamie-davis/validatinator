using System.Collections.Generic;

namespace Validatinator.RuleParsing
{
    internal class FirstErrorClause : ApplyIfRuleClause
    {
        #region Overrides of ApplyIfRuleClause

        internal override string Describe()
        {
            return "FirstError";
        }

        internal override bool GetRequireFirstError()
        {
            return true;
        }

        internal override bool GetRequireCleanInput()
        {
            return false;
        }

        internal override IEnumerable<EntityReference> GetMatches()
        {
            yield break;
        }
        #endregion
    }
}