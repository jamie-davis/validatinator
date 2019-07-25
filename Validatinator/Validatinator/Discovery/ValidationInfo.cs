using System;
using System.Reflection;

namespace Validatinator.Discovery
{
    public sealed class ValidationInfo
    {
        public Type ContainingType { get; }
        public MethodInfo Method { get; }

        public ValidationInfo(MethodInfo method)
        {
            ContainingType = method.DeclaringType;
            Method = method;
        }
    }
}