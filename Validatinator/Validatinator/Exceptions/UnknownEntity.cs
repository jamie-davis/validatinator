using System;

namespace Validatinator.Exceptions
{
    public class UnknownEntity : Exception
    {
        public Type Type { get; }

        public UnknownEntity(Type type) : base("Cannot determine the entity from the type")
        {
            Type = type;
        }
    }
}