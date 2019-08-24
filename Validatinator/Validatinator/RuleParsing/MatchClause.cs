using System.Collections.Generic;

namespace Validatinator.RuleParsing
{
    internal class MatchClause : ApplyIfRuleClause
    {
        public MatchClause(EntityReference entityReference)
        {
            EntityReference = entityReference;
        }

        internal EntityReference EntityReference { get; }

        #region Overrides of ApplyIfRuleClause

        internal override string Describe()
        {
            return $"Match({EntityReference.Describe()})";
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
            yield return EntityReference;
        }

        #endregion
    }
}