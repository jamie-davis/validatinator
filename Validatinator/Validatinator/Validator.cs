using System.Collections.Generic;

namespace Validatinator
{
    public class Validator
    {
        private readonly IValidationCatalog _validations;
        private List<object> _entities = new List<object>();

        public Validator(IValidationCatalog validations)
        {
            _validations = validations;
        }

        public void AddEntity(object entity)
        {
            if (!_entities.Contains(entity))
                _entities.Add(entity);
        }

        public void RemoveEntity(object entity)
        {
            if (_entities.Contains(entity))
                _entities.Remove(entity);
        }

    }
}
