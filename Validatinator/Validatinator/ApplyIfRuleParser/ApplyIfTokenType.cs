namespace Validatinator.ApplyIfRuleParser
{
    internal enum ApplyIfTokenType
    {
        Match,
        CleanInput,
        FirstError,
        OpenParen,
        CloseParen,
        Identifier,
        Error,
        Period,
        Terminator,
        Comma
    }
}