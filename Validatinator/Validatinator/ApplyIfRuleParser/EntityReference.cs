namespace Validatinator.ApplyIfRuleParser
{
    internal class EntityReference
    {
        public EntityReference(string entity, string field)
        {
            Entity = entity;
            Field = field;
        }

        public string Entity { get; }
        public string Field { get; }

        public string Describe()
        {
            return $"{Entity}.{Field}";
        }
    }
}