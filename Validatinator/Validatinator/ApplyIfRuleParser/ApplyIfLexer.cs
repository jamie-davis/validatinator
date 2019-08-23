using System.Collections.Generic;
using System.Linq;
using Validatinator.LexerUtilities;

namespace Validatinator.ApplyIfRuleParser
{
    internal static class ApplyIfLexer
    {
        private static readonly Dictionary<string, ApplyIfToken> Tokens = new Dictionary<string, ApplyIfToken>
        {
            {"match", new ApplyIfToken(ApplyIfTokenType.Match, "match")},
            {"firsterror", new ApplyIfToken(ApplyIfTokenType.FirstError, "firsterror")},
            {"cleaninput", new ApplyIfToken(ApplyIfTokenType.CleanInput, "cleaninput")},
            {"(", new ApplyIfToken(ApplyIfTokenType.OpenParen, "(")},
            {")", new ApplyIfToken(ApplyIfTokenType.CloseParen, ")")},
            {".", new ApplyIfToken(ApplyIfTokenType.Period, ".")},
            {",", new ApplyIfToken(ApplyIfTokenType.Comma, ",")},
        };

        private static string _terminators = "().,";

        public static IEnumerable<ApplyIfToken> Analyse(string source)
        {
            var stringKeeper = new StringKeeper(source);
            while (!stringKeeper.IsEmpty)
            {
                if (stringKeeper.IsWhitespace)
                {
                    stringKeeper = stringKeeper.Take();
                    continue;
                }

                StringKeeper newStringKeeper;
                ApplyIfToken token;
                if (TryTake(stringKeeper, Tokens, out newStringKeeper, out token)
                    || TryTakeIdentifier(stringKeeper, out newStringKeeper, out token))
                {
                    stringKeeper = newStringKeeper;
                    yield return token;
                }
                else 
                {
                    var finish = new StringKeeper(stringKeeper);
                    while (!finish.IsEmpty)
                        finish = finish.Take();
                    yield return new ApplyIfToken(ApplyIfTokenType.Error, finish.Difference(stringKeeper));
                    stringKeeper = finish;
                }
            }
        }

        private static bool TryTake(StringKeeper stringKeeper, Dictionary<string, ApplyIfToken> tokens, out StringKeeper newStringKeeper, out ApplyIfToken token)
        {
            var thisToken = tokens.FirstOrDefault(x => stringKeeper.IsNext(x.Key, false));
            if (thisToken.Key != null)
            {
                var work = stringKeeper.Take(thisToken.Key.Length);
                newStringKeeper = work;
                token = thisToken.Value;
                return true;
            }

            newStringKeeper = stringKeeper;
            token = null;
            return false;
        }

        private static bool TryTakeIdentifier(StringKeeper stringKeeper, out StringKeeper newStringKeeper, out ApplyIfToken token)
        {
            var work = new StringKeeper(stringKeeper);
            var value = string.Empty;
            while (!IsTerminator(work))
            {
                value += work.Next;
                work = work.Take();
            }

            if (!string.IsNullOrWhiteSpace(value))
            {
                newStringKeeper = work;
                token = new ApplyIfToken(ApplyIfTokenType.Identifier, value);
                return true;
            }
            
            newStringKeeper = stringKeeper;
            token = null;
            return false;
        }

        private static bool IsTerminator(StringKeeper stringKeeper)
        {
            return stringKeeper.IsEmpty || stringKeeper.IsWhitespace || _terminators.Contains(stringKeeper.Next.ToString());
        }
    }
}
