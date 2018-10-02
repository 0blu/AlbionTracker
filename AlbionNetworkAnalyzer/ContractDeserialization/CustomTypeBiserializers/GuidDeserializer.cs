using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers
{
    public class GuidDeserializer : ICustomTypeDeserializer<Guid>
    {
        public Guid Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return new Guid((byte[])obj);
        }
    }
}
