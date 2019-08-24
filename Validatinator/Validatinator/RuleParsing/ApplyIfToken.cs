namespace Validatinator.RuleParsing
{
    internal class ApplyIfToken
    {
        public string Text { get; set; }
        public ApplyIfTokenType TokenType { get; set; }

        public ApplyIfToken(ApplyIfTokenType tokenType, string text)
        {
            TokenType = tokenType;
            Text = text;
        }
    }
}