using System.Reflection;
using Validatinator.Discovery;

namespace Validatinator
{
    public static class ValidationSeeker
    {
        public static IValidationCatalog Seek(params Assembly[] assemblies)
        {
            var catalog = new ValidationCatalog();
            foreach (var assembly in assemblies)
            {
                catalog.AddValidations(ValidationFinder.ScanAssembly(assembly));
            }
            return catalog;
        }
    }
}
