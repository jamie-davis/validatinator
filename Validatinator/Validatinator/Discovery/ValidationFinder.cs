using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Validatinator.Attributes;

namespace Validatinator.Discovery
{
    public static class ValidationFinder
    {
        public static IEnumerable<ValidationInfo> ScanAssembly(Assembly assembly, Func<Type, bool> func = null)
        {
            var types = assembly.DefinedTypes.Where(t => t.IsClass && t.GetCustomAttribute<ValidationAttribute>() != null && !t.ContainsGenericParameters);
            if (func != null)
                types = types.Where(t => func(t));

            var methods = types.SelectMany(t => t.DeclaredMethods
                    .Where(m => m.IsStatic && m.IsPublic && m.ReturnType == typeof(ValidationResult) &&
                                !m.ContainsGenericParameters));

            return methods.Select(m => new ValidationInfo(m)).ToList();
        }
    }
}
