using System;
using System.Collections.Generic;
using System.Data;
using AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System;

namespace AlbionNetworkAnalyzer.ContractDeserialization
{
    public class SimpleCustomTypeDeserializerRegistry : ICustomTypeDeserializerRegistry
    {
        private readonly Dictionary<Type, object> _storedInstances = new Dictionary<Type, object>();

        public void RegisterCustomTypeDeserializer<T, TDeserializer>() where TDeserializer : ICustomTypeDeserializer<T>, new()
        {
            RegisterCustomTypeDeserializer(new TDeserializer());
        }

        public void RegisterCustomTypeDeserializer<T>(ICustomTypeDeserializer<T> instance)
        {
            _storedInstances.Add(typeof(T), instance);
        }

        public ICustomTypeDeserializer<T> GetCustomTypeDeserializer<T>()
        {
            if (_storedInstances.TryGetValue(typeof(T), out object instance))
                return (ICustomTypeDeserializer<T>) instance;

            if (typeof(T).IsEnum)
                return SimpleCastDeserializer<T>.Instance;

            throw new NoNullAllowedException($"No deserializer for '{typeof(T)}' was registerd");

        }
    }
}
