using System;
using Validatinator.RuleParsing;

namespace Validatinator.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FieldAttribute : Attribute
    {
        public string FieldName { get; }

        internal ApplyIfRule Rule {get;}

        public FieldAttribute(string fieldName, string rule = null)
        {
            FieldName = fieldName;
            if (rule != null)
            {
                Rule = ApplyIfParser.Parse(rule);
                if (!Rule.Valid)
                    throw new Exception($"{FieldName}: {ApplyIfParser.Parse(rule).Error} ({rule})");
                if (Rule.RequireCleanInput || Rule.RequireFirstError)
                {
                    throw new Exception($"{FieldName}: Only match rules are allowed on fields. ({rule})");
                }
            }
        }
    }
}