using System;
using Validatinator.Exceptions;
using Validatinator.RuleParsing;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidationFieldAttribute : Attribute
    {
        internal EntityReference Field { get; }

        public ValidationFieldAttribute(string fieldName)
        {
            var result = EntityReferenceParser.Parse(fieldName);
            if (!result.Valid)
                throw new EntityReferenceInvalid(result.Error, fieldName);
            Field = result.EntityReference;
        }
    }
}