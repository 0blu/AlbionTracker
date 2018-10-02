using System.Reflection;
using Albion.Common.Time;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers
{
    public class GameTimeStampDeserializer : ICustomTypeDeserializer<GameTimeStamp>
    {
        public GameTimeStamp Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return new GameTimeStamp(registry.GetCustomTypeDeserializer<long>().Deserialize(registry, obj, fieldInfo));
        }
    }
}
