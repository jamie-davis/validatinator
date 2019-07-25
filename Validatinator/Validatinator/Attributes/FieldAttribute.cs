using System;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FieldAttribute : Attribute
    {
        public string FieldName { get; }

        public FieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }
    }
}