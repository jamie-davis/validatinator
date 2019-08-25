using System;
using System.Reflection;
using Validatinator.Attributes;
using Validatinator.RuleParsing;

namespace Validatinator.Discovery
{
    internal class ValidationFieldInfo
    {
        public ValidationFieldInfo(FieldAttribute attrib, ParameterInfo parameter)
        {
            Name = attrib.EntityReference;
            Type = parameter.ParameterType;
        }

        public EntityReference Name { get; }
        public Type Type { get; }
    }
}