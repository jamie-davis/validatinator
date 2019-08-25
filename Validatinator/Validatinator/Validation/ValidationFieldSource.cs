using Validatinator.Discovery;

namespace Validatinator.Validation
{
    internal class ValidationFieldSource
    {
        public ValidationFieldSource(object target, ValidationFieldInfo field, TargetFieldInstance targetField)
        {
            Target = target;
            Field = field;
            TargetField = targetField;
        }

        public object Target { get; }
        public ValidationFieldInfo Field { get; }
        public TargetFieldInstance TargetField { get; }
    }
}