using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Albion.Common;
using Albion.Common.Time;

namespace AlbionTracker.Albion
{
    internal class EntityManager
    {
        private readonly ConcurrentDictionary<long/*EntityId*/, GameObject> _knownEntities = new ConcurrentDictionary<long, GameObject>();

        public void AddEntity(long objectId, string name, GameObject.GameObjectType objectType, GameObject.GameObjectSubType objectSubType)
        {
            if (_knownEntities.ContainsKey(objectId))
                return;

            GameObject gameObject = new GameObject(objectId)
            {
                Name = name,
                ObjectType = objectType,
                ObjectSubType = objectSubType
            };

            _knownEntities.TryAdd(objectId, gameObject);
            OnAddEntitiy?.Invoke(gameObject);
        }

        public void RemoveAll()
        {
            _knownEntities.Clear();
        }
        
        /*
        public GameObject GetEntity(long objectId)
        {
            if (!_knownEntities.TryGetValue(objectId, out var entity))
                throw new NoNullAllowedException($"Cant find entitiy '{objectId}'");
            return entity;
        }
        */

        public IEnumerable<GameObject> GetEntities()
        {
            return new List<GameObject>(_knownEntities.Values);
        }

        public event Action<GameObject> OnAddEntitiy;

        public void HealthUpdate(
            long objectId,
            GameTimeStamp packageTimeStamp,
            float packageHealthChange,
            float packageNewHealthValue,
            EffectType packageEffectType,
            EffectOrigin packageEffectOrigin,
            long packageCauserId,
            int packageCausingSpellType
            )
        {
            OnHealthUpdate?.Invoke(
                objectId,
                packageTimeStamp,
                packageHealthChange,
                packageNewHealthValue,
                packageEffectType,
                packageEffectOrigin,
                packageCauserId,
                packageCausingSpellType
                );
        }

        public event Action<long, GameTimeStamp, float, float, EffectType, EffectOrigin, long, int> OnHealthUpdate;
    }
}
