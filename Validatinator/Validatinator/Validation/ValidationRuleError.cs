namespace Validatinator.Validation
{
    internal class ValidationRuleError
    {
        public ValidationRuleError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}