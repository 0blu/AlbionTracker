using System;
using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System.Numbes
{
    public class DoubleDeserializer : ICustomTypeDeserializer<double>
    {
        public double Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return Convert.ToDouble(obj);
        }
    }
}
