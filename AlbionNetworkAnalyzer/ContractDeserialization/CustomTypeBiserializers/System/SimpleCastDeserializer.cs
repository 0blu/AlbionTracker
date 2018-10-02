using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers.System
{
    public class SimpleCastDeserializer<T> : ICustomTypeDeserializer<T>
    {
        public static readonly SimpleCastDeserializer<T> Instance = new SimpleCastDeserializer<T>();
        
        public T Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return (T) obj;
        }
    }
}
