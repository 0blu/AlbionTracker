using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class Int64Deserializer : ICustomTypeDeserializer<long>
    {
        public long Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToInt64(obj);
        }
    }
}
