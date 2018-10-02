using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class Int16Deserializer : ICustomTypeDeserializer<short>
    {
        public short Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToInt16(obj);
        }
    }
}
