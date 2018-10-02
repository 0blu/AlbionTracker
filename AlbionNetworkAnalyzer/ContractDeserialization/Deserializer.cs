using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albion.Common.Photon;

namespace AlbionNetworkAnalyzer.ContractDeserialization
{
    public class Deserializer
    {
        public struct ClassFieldInfo
        {
            public FieldInfo FieldInfo;
            public bool IsOptional;
        }

        private readonly Dictionary<Type, Dictionary<byte, ClassFieldInfo>> _contractTypeToClassInfoInfos = new Dictionary<Type, Dictionary<byte, ClassFieldInfo>>();

        private Dictionary<byte, ClassFieldInfo> GetAllClassFieldInfos(Type contractType)
        {
            byte dataIndex = 0;

            Dictionary<byte, ClassFieldInfo> classFieldInfos = new Dictionary<byte, ClassFieldInfo>();

            foreach (var fieldInfo in contractType.GetFields().OrderBy(f => f.MetadataToken))
            {
                var attr = (DataFieldAttribute)fieldInfo.GetCustomAttribute(typeof(DataFieldAttribute));

                byte currentDataIndex;
                if (attr?.IsCustomFieldIndexSet ?? false)
                {
                    currentDataIndex = attr.FieldIndex;
                }
                else
                {
                    currentDataIndex = dataIndex++;
                }

                var currentIsOptional = attr?.IsOptional ?? true;

                if (classFieldInfos.ContainsKey(currentDataIndex))
                {
                    throw new Exception($"Field[{currentDataIndex}]({fieldInfo.Name}@{fieldInfo.DeclaringType.FullName}) has the same data index as another field");
                }

                classFieldInfos.Add(currentDataIndex, new ClassFieldInfo
                {
                    FieldInfo = fieldInfo,
                    IsOptional = currentIsOptional,
                });
            }

            return classFieldInfos;
        }

        public void RegisterContract(Type contractType)
        {
            _contractTypeToClassInfoInfos.Add(contractType, GetAllClassFieldInfos(contractType));
        }

        public object FromDictionary(ICustomTypeDeserializerRegistry registry, Type contractType, Dictionary<byte, object> parameters)
        {
            object obj = Activator.CreateInstance(contractType);

            MethodInfo getCustomTypeBiserializer = registry.GetType().GetMethod("GetCustomTypeDeserializer");

            foreach (var indexToType in _contractTypeToClassInfoInfos[contractType])
            {
                if (!parameters.TryGetValue(indexToType.Key, out object paramterObject))
                    continue;

                var gen = getCustomTypeBiserializer.MakeGenericMethod(new Type[] { indexToType.Value.FieldInfo.FieldType });
                var customTypeBiserializerInstance = gen.Invoke(registry, new object[0]);
                var customTypeBiserializerType = customTypeBiserializerInstance.GetType();
                var deserializionMethod = customTypeBiserializerType.GetMethod("Deserialize");
                var result = deserializionMethod.Invoke(customTypeBiserializerInstance, new object[] { registry, paramterObject, indexToType.Value.FieldInfo });
                indexToType.Value.FieldInfo.SetValue(obj, result);
            }

            return obj;
        }

        public T FromDictionary<T>(ICustomTypeDeserializerRegistry registry, Dictionary<byte, object> parameters) where T: new()
        {
            return (T)FromDictionary(registry, typeof(T), parameters);
        }
    }
}
