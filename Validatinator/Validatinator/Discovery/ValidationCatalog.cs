﻿using System.Collections.Generic;
using System.Linq;

namespace Validatinator.Discovery
{
    internal class ValidationCatalog : IValidationCatalog
    {
        private List<ValidationInfo> _all = new List<ValidationInfo>();
        private Dictionary<string, List<ValidationInfo>> _validationsByField = new Dictionary<string, List<ValidationInfo>>();

        internal ValidationCatalog()
        {
            
        }

        public void AddValidations(IEnumerable<ValidationInfo> validations)
        {
            var newValidations = validations.Where(v => !_all.Any(a => a.Method == v.Method)).ToList();
            _all.AddRange(newValidations);
            Index(newValidations);
        }

        private void Index(List<ValidationInfo> newValidations)
        {
            var fields = newValidations.SelectMany(v => v.Fields.Select(f => new { f.Name, Validation = v }));
            foreach (var field in fields)
            {
                if (!_validationsByField.TryGetValue(field.Name.Describe(), out var validations))
                {
                    validations = new List<ValidationInfo>();
                    _validationsByField[field.Name.Describe()] = validations;
                }

                if (!validations.Contains(field.Validation))
                    validations.Add(field.Validation);
            }
        }

        IEnumerable<ValidationInfo> IValidationCatalog.Validations()
        {
            return _all.ToList();
        }
    }
}
