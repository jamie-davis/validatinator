namespace Validatinator.RuleParsing
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