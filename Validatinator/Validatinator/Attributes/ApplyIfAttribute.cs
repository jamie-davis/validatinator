using System;
using Validatinator.ApplyIfRuleParser;

namespace Validatinator.Attributes
{
    /// <summary>
    /// This attribute prevents validations from being used if they are not appropriate.
    /// <para/>
    /// For example, if you have a drink order, the size of each drink may need to be
    /// validated against its type. Because the order contains multiple drink rows, we need
    /// to tell the validation framework that  it is not to apply the size validation across
    /// different rows (otherwise it will validate all drinks against all sizes).
    /// <para/>
    /// In that example, we would use the rule [ApplyIf("Match(LineItemId)")] which assumes all of the
    /// rows will have a "LineItemId" value. Only entities with a "LineItemId" field will be eligible,
    /// and the validation will only apply when all of the input fields come from entities with the
    /// same "LineItemId" value.
    /// <para/>
    /// Another rule type is [ApplyIf("FirstError")], which prevents the validation from running if
    /// there are any other errors flagged.
    /// <para/>
    /// This contrasts with [ApplyIf("CleanInput)"] which will prevent the validation from being run
    /// if one of the involved field sources already has an error, and [ApplyIf("CleanEntity")]
    /// which prevents the validation from being run if one of the entities involved already has
    /// an error.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApplyIfAttribute : Attribute
    {
        internal ApplyIfRule Rule { get; }

        public ApplyIfAttribute(string rule)
        {
            Rule = ApplyIfParser.Parse(rule);
            if (!Rule.Valid)
                throw new Exception($"ApplyIf: {ApplyIfParser.Parse(rule).Error} ({rule})");
        }
    }
}
