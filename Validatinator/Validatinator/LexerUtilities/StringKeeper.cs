using System;

namespace Validatinator.LexerUtilities
{
    public class StringKeeper
    {
        private readonly int _index;
        private readonly string _text;

        public StringKeeper(string text, int index = 0)
        {
            _text = text;
            _index = index;
        }

        public StringKeeper(StringKeeper stringKeeper)
        {
            _index = stringKeeper._index;
            _text = stringKeeper._text;
        }

        public bool IsEmpty => _text == null || _index >= _text.Length;
        public bool IsWhitespace => !IsEmpty && char.IsWhiteSpace(Next);

        public char Next => _text[_index];
        public StringKeeper Take(int chars = 1) { return new StringKeeper(_text, _index + chars); }

        public string Difference(StringKeeper old)
        {
            if (old._text != _text) return string.Empty;
            if (old._index > _index) return string.Empty;

            return _text.Substring(old._index, _index - old._index);
        }

        public bool IsNext(string value, bool caseSensitive)
        {
            if (value.Length <= _text.Length - _index)
            {
                var stringComparison = caseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                return string.Compare(_text.Substring(_index, value.Length), value, stringComparison) == 0;
            }

            return false;
        }
    }
}
