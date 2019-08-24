using System.Collections.Generic;
using System.Linq;

namespace Validatinator.RuleParsing
{
    internal class RuleSequenceClause : ApplyIfRuleClause
    {
        public RuleSequenceClause(ApplyIfRuleClause left, ApplyIfRuleClause right)
        {
            Left = left;
            Right = right;
        }

        internal ApplyIfRuleClause Left { get; }
        internal ApplyIfRuleClause Right { get; }

        #region Overrides of ApplyIfRuleClause

        internal override string Describe()
        {
            return $"{Left.Describe()},{Right.Describe()}";
        }

        internal override bool GetRequireFirstError()
        {
            return Left.GetRequireFirstError() || Right.GetRequireFirstError();
        }

        internal override bool GetRequireCleanInput()
        {
            return Left.GetRequireCleanInput() || Right.GetRequireCleanInput();
        }

        internal override IEnumerable<EntityReference> GetMatches()
        {
            return Left.GetMatches().Concat(Right.GetMatches());
        }

        #endregion
    }
}