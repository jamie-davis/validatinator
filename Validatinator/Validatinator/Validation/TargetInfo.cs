using System.Collections.Generic;
using System.Linq;

namespace Validatinator.Validation
{
    internal sealed class TargetInfo
    {
        internal TargetInfo(IEnumerable<TargetEntityInfo> entities)
        {
            Entities = entities.ToList();
        }

        internal IEnumerable<TargetEntityInfo> Entities { get; }
    }
}
 