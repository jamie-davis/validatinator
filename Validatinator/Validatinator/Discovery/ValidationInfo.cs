using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Validatinator.Attributes;

namespace Validatinator.Discovery
{
    public sealed class ValidationInfo
    {
        private List<ValidationFieldInfo> _fields;

        internal Type ContainingType { get; }
        internal MethodInfo Method { get; }
        internal IEnumerable<ValidationFieldInfo> Fields => _fields.Select(f => f);

        internal ValidationInfo(MethodInfo method)
        {
            ContainingType = method.DeclaringType;
            Method = method;
            ExtractFields(method);
        }

        private void ExtractFields(MethodInfo method)
        {
            var parameters = method.GetParameters();
            _fields = parameters.Select(p => new {Attrib = p.GetCustomAttribute<FieldAttribute>(), Parameter = p})
                .Where(p => p.Attrib != null)
                .Select(p => new ValidationFieldInfo(p.Attrib, p.Parameter))
                .ToList();
        }
    }
}