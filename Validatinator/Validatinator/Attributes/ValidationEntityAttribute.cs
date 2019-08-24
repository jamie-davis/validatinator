using System;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidationEntityAttribute : Attribute
    {
        public string EntityName { get; }

        public ValidationEntityAttribute(string entityName)
        {
            EntityName = entityName;
        }
    }
}
