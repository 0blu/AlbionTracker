using System.Reflection;
using Albion.Common.Math;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers
{
    public class FixPointDeserializer : ICustomTypeDeserializer<FixPoint>
    {
        public FixPoint Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return FixPoint.FromInternalValue(registry.GetCustomTypeDeserializer<long>().Deserialize(registry, obj, fieldInfo));
        }
    }
}
