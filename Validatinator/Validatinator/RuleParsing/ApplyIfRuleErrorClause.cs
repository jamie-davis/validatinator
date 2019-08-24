using System.Collections.Generic;

namespace Validatinator.RuleParsing
{
    internal sealed class ApplyIfRuleErrorClause : ApplyIfRuleClause
    {
        public string Error { get; }

        public ApplyIfRuleErrorClause(string error)
        {
            Error = error;
        }

        #region Overrides of ApplyIfRuleClause

        internal override string Describe()
        {
            return $"Error({Error})";
        }

        internal override bool GetRequireFirstError()
        {
            return false;
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