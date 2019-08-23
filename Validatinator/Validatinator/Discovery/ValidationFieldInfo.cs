using System;
using System.Reflection;
using Validatinator.Attributes;

namespace Validatinator.Discovery
{
    internal class ValidationFieldInfo
    {
        public ValidationFieldInfo(FieldAttribute attrib, ParameterInfo parameter)
        {
            Name = attrib.FieldName;
            Type = parameter.ParameterType;
        }

        public string Name { get; }
        public Type Type { get; }
    }
}