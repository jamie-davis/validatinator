using System.Collections.Generic;
using Validatinator.Discovery;

namespace Validatinator
{
    public interface IValidationCatalog
    {
        IEnumerable<ValidationInfo> Validations();
    }
}