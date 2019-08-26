using System;

namespace Validatinator.Validation
{
    internal static class TypeCompatibility
    {
        public static bool Check(ValidationFieldSource source)
        {
            return Check(source.Field.Type, source.TargetField.TargetFieldInfo.Type);
        }

        public static bool Check(Type left, Type right)
        {

        }
    }
}