using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class Int32Deserializer : ICustomTypeDeserializer<int>
    {
        public int Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToInt32(obj);
        }
    }
}
