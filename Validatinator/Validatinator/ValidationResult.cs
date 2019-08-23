namespace Validatinator
{
    public class ValidationResult
    {
        private ValidationResult()
        {
            IsValid = true;
        }

        private ValidationResult(string message)
        {
            IsValid = false;
            Message = message;
        }

        public string Message { get; }

        public bool IsValid { get; }

        public static ValidationResult Valid { get; } = new ValidationResult();

        public static ValidationResult Error(string message)
        {
            return new ValidationResult(message);
        }
    }
}
