using System;
using System.Linq;
using System.Reflection;
using Albion.Common.Math;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers
{
    public class FixPointArrayDeserializer : ICustomTypeDeserializer<FixPoint[]>
    {
        public FixPoint[] Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Array.ConvertAll((long[])obj, FixPoint.FromInternalValue);
        }
    }
}
