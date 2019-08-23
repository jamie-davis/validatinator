namespace Validatinator.ApplyIfRuleParser
{
    internal class FieldReference
    {
        public FieldReference(string entity, string field)
        {
            Entity = entity;
            Field = field;
        }

        public string Entity { get; }
        public string Field { get; }
    }
}