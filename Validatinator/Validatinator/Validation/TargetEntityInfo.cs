using System.Collections.Generic;
using System.Linq;

namespace Validatinator.Validation
{
    internal sealed class TargetEntityInfo
    {
        internal TargetEntityInfo(string entity, IEnumerable<TargetFieldInfo> fields)
        {
            Entity = entity;
            Fields = fields.ToList();
        }

        internal string Entity { get; }
        internal IEnumerable<TargetFieldInfo> Fields { get; }
    }
}