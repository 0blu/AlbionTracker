using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class SingleDeserializer : ICustomTypeDeserializer<float>
    {
        public float Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToSingle(obj);
        }
    }
}
