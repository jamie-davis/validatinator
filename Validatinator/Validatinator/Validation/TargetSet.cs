using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Validatinator.RuleParsing;

namespace Validatinator.Validation
{
    internal class TargetSet
    {
        private readonly Dictionary<Type, TargetEntityInfo> _targetData = new Dictionary<Type, TargetEntityInfo>();

        private readonly List<object> _targets = new List<object>();
        private readonly Dictionary<EntityReference, List<TargetFieldInstance>> _fieldRefs = new Dictionary<EntityReference, List<TargetFieldInstance>>();

        public void Add(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (_targets.Contains(target))
                return;

            _targets.Add(target);

            Index(target);
        }

        private void Index(object target)
        {
            var targetType = target.GetType();
            if (!_targetData.TryGetValue(targetType, out var info))
            {
                info = TargetAnalyser.Analyse(targetType);
                _targetData[targetType] = info;
            }

            foreach (var field in info.Fields)
            {
                if (!_fieldRefs.TryGetValue(field.Field, out var instances))
                {
                    instances = new List<TargetFieldInstance>();
                    _fieldRefs[field.Field] = instances;
                }

                if (!instances.Any(i => ReferenceEquals(i.Target, target)))
                {
                    instances.Add(new TargetFieldInstance(field, target));
                }
            }
        }

        public void Remove(object target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (_targets.Contains(target))
                _targets.Remove(target);

            Deindex(target);
        }

        private void Deindex(object target)
        {
            var targetType = target.GetType();
            if (_targetData.TryGetValue(targetType, out var info))
            {
                foreach (var field in info.Fields)
                {
                    if (_fieldRefs.TryGetValue(field.Field, out var references))
                    {
                        var entityFields = references.Where(f => ReferenceEquals(f.Target, target)).ToList();
                        foreach (var instance in entityFields)
                        {
                            references.Remove(instance);
                        }
                    }
                }
            }
        }

        public bool Contains(object target)
        {
            return _targets.Contains(target) || _fieldRefs.SelectMany(r => r.Value.Select(i => i.Target)).Any(t => ReferenceEquals(t, target));
        }

        internal IEnumerable<TargetFieldInstance> GetFields(EntityReference fieldRef)
        {
            if (_fieldRefs.TryGetValue(fieldRef, out var instances))
            {
                return instances.ToList();
            }
            
            return new List<TargetFieldInstance>();
        }
    }
}