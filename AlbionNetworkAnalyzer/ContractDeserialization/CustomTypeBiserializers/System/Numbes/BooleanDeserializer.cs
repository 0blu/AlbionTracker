using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class BooleanDeserializer : ICustomTypeDeserializer<bool>
    {
        public bool Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToBoolean(obj);
        }
    }
}
