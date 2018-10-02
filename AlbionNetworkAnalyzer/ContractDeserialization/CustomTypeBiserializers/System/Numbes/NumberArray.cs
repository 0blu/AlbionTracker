using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class NumberArray<T> : ICustomTypeDeserializer<T[]> where T : IComparable<T>
    {
        public T[] Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            throw new NotImplementedException();
        }
    }
}
