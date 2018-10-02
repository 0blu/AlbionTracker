using System.Reflection;
using Albion.Common.Time;

namespace AlbionNetworkAnalyzer.ContractDeserialization.CustomTypeBiserializers
{
    public class GameTimeSpanDeserializer : ICustomTypeDeserializer<GameTimeSpan>
    {
        public GameTimeSpan Deserialize(ICustomTypeDeserializerRegistry registry, object obj, FieldInfo fieldInfo)
        {
            return new GameTimeSpan(registry.GetCustomTypeDeserializer<long>().Deserialize(registry, obj, fieldInfo));
        }
    }
}
