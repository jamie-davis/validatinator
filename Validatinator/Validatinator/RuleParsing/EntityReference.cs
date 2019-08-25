using System.Diagnostics;

namespace Validatinator.RuleParsing
{
    [DebuggerDisplay("Ref: {Entity}.{Field}")]
    internal class EntityReference
    {
        public string Entity { get; }

        public string Field { get; }

        public EntityReference(string entity, string field)
        {
            Entity = entity;
            Field = field;
        }

        public string Describe()
        {
            return $"{Entity}.{Field}";
        }

        protected bool Equals(EntityReference other)
        {
            return Entity == other.Entity && Field == other.Field;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EntityReference) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Entity != null ? Entity.GetHashCode() : 0) * 397) ^ (Field != null ? Field.GetHashCode() : 0);
            }
        }
    }
}