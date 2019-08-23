using System.Collections.Generic;

namespace Validatinator.ApplyIfRuleParser
{
    internal class ApplyIfTokenKeeper
    {
        private List<ApplyIfToken> _tokens;
        private int _index;
        private static readonly ApplyIfToken Terminator = new ApplyIfToken(ApplyIfTokenType.Terminator, null);

        public ApplyIfTokenKeeper(List<ApplyIfToken> tokens)
        {
            _tokens = tokens;
            _index = 0;
        }

        public ApplyIfTokenKeeper(ApplyIfTokenKeeper tokenKeeper)
        {
            _tokens = tokenKeeper._tokens;
            _index = tokenKeeper._index;
        }

        public bool Finished
        {
            get { return _index >= _tokens.Count; }
        }

        public ApplyIfToken Next => Finished ? Terminator : _tokens[_index];

        public ApplyIfToken Take()
        {
            var next = Next;
            if (next.TokenType != ApplyIfTokenType.Terminator)
                ++_index;
            return next;
        }

        public void Swap(ApplyIfTokenKeeper tokenKeeper)
        {
            var oldTokens = _tokens;
            _tokens = tokenKeeper._tokens;

            var oldIndex = _index;
            _index = tokenKeeper._index;

            tokenKeeper._tokens = oldTokens;
            tokenKeeper._index = oldIndex;
        }
    }
}