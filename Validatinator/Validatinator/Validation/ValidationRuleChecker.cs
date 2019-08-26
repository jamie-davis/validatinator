using System.Collections.Generic;

namespace Validatinator.Validation
{
    internal static class ValidationRuleChecker
    {
        public static IEnumerable<RequiredValidation> Filter(List<CandidateValidation> allPossible)
        {
            foreach (var candidateValidation in allPossible)
            {
                yield return new RequiredValidation(candidateValidation);
            }
        }

        public static IEnumerable<ValidationRuleError> Check(CandidateValidation candidate)
        {
            foreach (var source in candidate.Fields)
            {
                if (!TypeCompatibility.Check(source))
                {
                    var errorMessage = DescribeTypeMismatch(candidate, source);
                    yield return new ValidationRuleError(errorMessage);
                }
            }
        }

        private static string DescribeTypeMismatch(CandidateValidation candidate, ValidationFieldSource source)
        {
            var targetFieldRef = source.TargetField.TargetFieldInfo.Field.Describe();
            var targetType = source.Target.GetType().Name;
            var targetFieldType = source.TargetField.TargetFieldInfo.Type.Name;
            var validationDesc = $"{candidate.Validation.ContainingType.Name}.{candidate.Validation.Method.Name}";
            var validationParamRef = source.Field.Name;
            var validationParamType = source.Field.Type.Name;
            var errorMessage =
                $"{targetFieldRef} on {targetType} has type {targetFieldType} which is not compatible with validation {validationDesc}'s {validationParamRef} which requires {validationParamType}";
            return errorMessage;
        }
    }
}