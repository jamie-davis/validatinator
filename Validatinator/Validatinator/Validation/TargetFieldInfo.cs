using System;
using System.Reflection;
using Validatinator.Attributes;
using Validatinator.RuleParsing;

namespace Validatinator.Validation
{
    internal sealed class TargetFieldInfo
    {
        internal TargetFieldInfo(string entity, PropertyInfo field)
        {
            Entity = entity;

            var attribute = field.GetCustomAttribute<ValidationFieldAttribute>();
            if (attribute != null)
                Field = attribute.Field;
            else
                Field = new EntityReference(entity, field.Name);

            Type = field.PropertyType;
        }

        internal string Entity { get; }
        internal EntityReference Field { get; }
        internal Type Type { get; set; }
    }
}