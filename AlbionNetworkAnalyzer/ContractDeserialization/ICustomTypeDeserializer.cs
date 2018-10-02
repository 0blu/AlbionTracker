using System.Reflection;

namespace AlbionNetworkAnalyzer.ContractDeserialization
{
    public interface ICustomTypeDeserializer<out T>
    {
        T Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo);
    }
}
