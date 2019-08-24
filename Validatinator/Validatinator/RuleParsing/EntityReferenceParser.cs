using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validatinator.RuleParsing
{
    class EntityReferenceParser
    {
        public static EntityReferenceParseResult Parse(string entityRef)
        {
            var tokens = RuleLexer.Analyse(entityRef ?? string.Empty);
            var firstErr = tokens.FirstOrDefault(t => t.TokenType == ApplyIfTokenType.Error);
            if (firstErr != null)
            {
                var lexingError = $@"""{firstErr.Text}"" is not a valid token.";
                var name = new EntityReferenceParseResult(lexingError);
                return name;
            }

            var rule = PerformParse(tokens, out var error);
            if (!string.IsNullOrWhiteSpace(error))
                return new EntityReferenceParseResult(error);
            return rule;
        }

        private static EntityReferenceParseResult PerformParse(IEnumerable<ApplyIfToken> tokens, out string error)
        {
            var tokenKeeper = new RuleTokenKeeper(tokens.ToList());
            if (tokenKeeper.Finished)
            {
                error = "No entity reference found.";
                return null;
            }

            if (TryTakeEntityReference(tokenKeeper, out var reference))
            {
                error = null;
                return new EntityReferenceParseResult(reference);
            }

            error = "Unable to parse entity reference";
            return new EntityReferenceParseResult(error);
        }

        internal static bool TryTakeEntityReference(RuleTokenKeeper tokenKeeper, out EntityReference clause)
        {
            var next = tokenKeeper.Next;
            if (next.TokenType == ApplyIfTokenType.Identifier)
            {
                var work = new RuleTokenKeeper(tokenKeeper);
                var entity = work.Take().Text;

                if (work.Next.TokenType == ApplyIfTokenType.Period)
                {
                    work.Take();
                    if (work.Next.TokenType == ApplyIfTokenType.Identifier)
                    {
                        var field = work.Take().Text;
                        tokenKeeper.Swap(work);
                        clause = new EntityReference(entity, field);
                        return true;
                    }
                }

                clause = null;
                return false;
            }

            clause = null;
            return false;
        }
    }
}
