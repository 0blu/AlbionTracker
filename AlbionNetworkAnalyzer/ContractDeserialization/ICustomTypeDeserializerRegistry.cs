namespace AlbionNetworkAnalyzer.ContractDeserialization
{
    public interface ICustomTypeDeserializerRegistry
    {
        ICustomTypeDeserializer<T> GetCustomTypeDeserializer<T>();
    }
}
