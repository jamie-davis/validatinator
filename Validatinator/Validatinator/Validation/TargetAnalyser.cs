using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Validatinator.Attributes;
using Validatinator.Exceptions;

namespace Validatinator.Validation
{
    internal static class TargetAnalyser
    {
        internal static TargetEntityInfo Analyse(object target, string entityName = null)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), entityName);
            return Analyse(target.GetType());
        }

        internal static TargetEntityInfo Analyse(Type target, string entityName = null)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (entityName == null)
            {
                var entityAttrib = target.GetCustomAttribute<ValidationEntityAttribute>();
                if (entityAttrib != null)
                    entityName = entityAttrib.EntityName;
            }

            if (entityName == null)
            {
                throw new UnknownEntity(target);
            }

            return new TargetEntityInfo(entityName, GetFields(target, entityName));
        }

        private static IEnumerable<TargetFieldInfo> GetFields(Type target, string entity)
        {
            return target.GetProperties()
                .Where(p => p.GetCustomAttribute<NotValidatedAttribute>() == null)
                .Select(p => new TargetFieldInfo(entity, p));
        }
    }
}
