using System.Collections.Generic;
using System.Linq;

namespace Validatinator.ApplyIfRuleParser
{
    static class ApplyIfParser
    {
        internal static ApplyIfRule Parse(string applyIfSpec)
        {
            var tokens = ApplyIfLexer.Analyse(applyIfSpec ?? string.Empty);
            var firstErr = tokens.FirstOrDefault(t => t.TokenType == ApplyIfTokenType.Error);
            if (firstErr != null)
            {
                var lexingError = $@"""{firstErr.Text}"" is not a valid token.";
                var name = new ApplyIfRule(new ApplyIfRuleErrorClause($"ApplyIf rule could not be interpreted. (\"{firstErr.Text}\" is not valid.)"));
                name.FailedParse(lexingError);
                return name;
            }

            var rule = PerformParse(tokens, out var error);
            if (!string.IsNullOrWhiteSpace(error))
                rule.FailedParse(error);
            return rule;
        }

        private static ApplyIfRule PerformParse(IEnumerable<ApplyIfToken> tokens, out string error)
        {
            var tokenKeeper = new ApplyIfTokenKeeper(tokens.ToList());
            if (tokenKeeper.Finished)
            {
                error = null;
                return null;
            }

            var clause = TakeClause(tokenKeeper);

            if (clause is ApplyIfRuleErrorClause errorClause)
                error = errorClause.Error;
            else
                error = null;

            return new ApplyIfRule(clause);
        }

        private static ApplyIfRuleClause TakeClause(ApplyIfTokenKeeper tokenKeeper)
        {
            ApplyIfRuleClause clause;

            if (TryTakeRuleSequence(tokenKeeper, out clause)
            || TryTakeRuleClause(tokenKeeper, out clause))
            {
                if (tokenKeeper.Finished)
                    return clause;
            }

            return new ApplyIfRuleErrorClause("Unable to parse ApplyIf rule.");

        }

        private static bool TryTakeRuleClause(ApplyIfTokenKeeper tokenKeeper, out ApplyIfRuleClause clause)
        {
            if (TryTakeCleanInput(tokenKeeper, out clause)
                || TryTakeFirstError(tokenKeeper, out clause)
                || TryTakeMatch(tokenKeeper, out clause))
            {
                return true;
            }

            clause = null;
            return false;
        }

        private static bool TryTakeRuleSequence(ApplyIfTokenKeeper tokenKeeper, out ApplyIfRuleClause clause)
        {
            var work = new ApplyIfTokenKeeper(tokenKeeper);
            if (TryTakeRuleClause(work, out var left))
            {
                if (work.Next.TokenType == ApplyIfTokenType.Comma)
                {
                    ApplyIfRuleClause right;
                    work.Take();
                    if (TryTakeRuleSequence(work, out right) || TryTakeRuleClause(work, out right))
                    {
                        if (work.Finished)
                        {
                            tokenKeeper.Swap(work);
                            clause = new RuleSequenceClause(left, right);
                            return true;
                        }
                    }
                }
            }

            clause = null;
            return false;
        }

        private static bool TryTakeEntityReference(ApplyIfTokenKeeper tokenKeeper, out EntityReference clause)
        {
            var next = tokenKeeper.Next;
            if (next.TokenType == ApplyIfTokenType.Identifier)
            {
                var work = new ApplyIfTokenKeeper(tokenKeeper);
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

        private static bool TryTakeCleanInput(ApplyIfTokenKeeper tokenKeeper, out ApplyIfRuleClause clause)
        {
            var next = tokenKeeper.Next;
            if (next.TokenType == ApplyIfTokenType.CleanInput)
            {
                clause = new CleanInputClause();
                tokenKeeper.Take();
                return true;
            }

            clause = null;
            return false;
        }

        private static bool TryTakeFirstError(ApplyIfTokenKeeper tokenKeeper, out ApplyIfRuleClause clause)
        {
            var next = tokenKeeper.Next;
            if (next.TokenType == ApplyIfTokenType.FirstError)
            {
                clause = new FirstErrorClause();
                tokenKeeper.Take();
                return true;
            }

            clause = null;
            return false;
        }

        private static bool TryTakeMatch(ApplyIfTokenKeeper tokenKeeper, out ApplyIfRuleClause clause)
        {
            var next = tokenKeeper.Next;
            if (next.TokenType == ApplyIfTokenType.Match)
            {
                var work = new ApplyIfTokenKeeper(tokenKeeper);
                work.Take();

                if (work.Next.TokenType == ApplyIfTokenType.OpenParen)
                {
                    work.Take();

                    if (TryTakeEntityReference(work, out var entityReference))
                    {
                        if (work.Next.TokenType == ApplyIfTokenType.CloseParen)
                            work.Take();

                        tokenKeeper.Swap(work);
                        clause = new MatchClause(entityReference);
                        return true;
                    }
                }
            }

            clause = null;
            return false;
        }
    }
}
