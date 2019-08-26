namespace Validatinator.Validation
{
    internal sealed class TargetFieldInstance
    {
        public TargetFieldInstance(TargetFieldInfo targetFieldInfo, object target)
        {
            TargetFieldInfo = targetFieldInfo;
            Target = target;
        }

        internal TargetFieldInfo TargetFieldInfo { get; }
        internal object Target { get; }
    }
}