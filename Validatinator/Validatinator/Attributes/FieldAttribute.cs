using System;
using Validatinator.RuleParsing;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FieldAttribute : Attribute
    {
        internal EntityReference EntityReference { get; }

        internal ApplyIfRule Rule {get;}

        public FieldAttribute(string fieldName, string rule = null)
        {
            var entityReferenceParseResult = EntityReferenceParser.Parse(fieldName);
            if (!entityReferenceParseResult.Valid)
                throw new Exception($"{EntityReference}: {entityReferenceParseResult.Error}");

            EntityReference = entityReferenceParseResult.EntityReference;
            
            if (rule != null)
            {
                Rule = ApplyIfParser.Parse(rule);
                if (!Rule.Valid)
                    throw new Exception($"{EntityReference}: {ApplyIfParser.Parse(rule).Error} ({rule})");
                if (Rule.RequireCleanInput || Rule.RequireFirstError)
                {
                    throw new Exception($"{EntityReference}: Only match rules are allowed on fields. ({rule})");
                }
            }
        }
    }
}