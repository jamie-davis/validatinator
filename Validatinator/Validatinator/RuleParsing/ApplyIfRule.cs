using System.Collections.Generic;
using System.Linq;

namespace Validatinator.RuleParsing
{
    internal class ApplyIfRule
    {
        private readonly List<FieldReference> _matches;

        /// <summary>
        /// This will be true unless there was an error parsing the applyif rule.
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// This will be true if the clean input clause is in force.
        /// </summary>
        public bool RequireCleanInput { get; }

        /// <summary>
        /// This will be true if the first error clause is in force.
        /// </summary>
        public bool RequireFirstError { get; }

        /// <summary>
        /// A list of fields that must match across input entities.
        /// </summary>
        public IEnumerable<FieldReference> Matches => _matches;

        /// <summary>
        /// This will contain an error message if <see cref="Valid"/> is false.
        /// </summary>
        public string Error { get; private set; }

        public ApplyIfRule(ApplyIfRuleClause clause)
        {
            Valid = true;
            Root = clause;
            RequireFirstError = clause.GetRequireFirstError();
            RequireCleanInput = clause.GetRequireCleanInput();
            _matches = clause.GetMatches()
                .Select(m => new FieldReference(m.Entity, m.Field))
                .ToList();
        }

        internal ApplyIfRuleClause Root { get; }

        public void FailedParse(string error)
        {
            Valid = false;
            Error = error;
        }
    }
}