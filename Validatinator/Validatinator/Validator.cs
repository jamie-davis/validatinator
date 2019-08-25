using Validatinator.Validation;

namespace Validatinator
{
    public class Validator
    {
        private readonly IValidationCatalog _validations;
        private readonly TargetSet _entities = new TargetSet();

        public Validator(IValidationCatalog validations)
        {
            _validations = validations;
        }

        public void AddEntity(object entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(object entity)
        {
            _entities.Remove(entity);
        }

    }
}
