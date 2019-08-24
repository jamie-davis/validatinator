using System;

namespace Validatinator.Exceptions
{
    public class EntityReferenceInvalid : Exception
    {
        public string Error { get; }
        public string Reference { get; }

        public EntityReferenceInvalid(string error, string reference) : base("Unable to understand entity reference")
        {
            Error = error;
            Reference = reference;
        }
    }
}