namespace Validatinator.Validation
{
    internal sealed class TargetFieldInstance
    {
        public TargetFieldInstance(TargetFieldInfo targetFieldInfo, object target)
        {
            this.targetFieldInfo = targetFieldInfo;
            Target = target;
        }

        internal TargetFieldInfo targetFieldInfo { get; }
        internal object Target { get; }
    }
}