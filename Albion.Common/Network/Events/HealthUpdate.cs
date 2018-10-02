using Albion.Common.Photon;
using Albion.Common.Time;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.HealthUpdate)]
    public class HealthUpdate : EventWithObjectId
    {
        public GameTimeStamp TimeStamp;
        public float HealthChange;
        public float NewHealthValue;
        public EffectType EffectType;
        public EffectOrigin EffectOrigin;
        public long CauserId;
        public int CausingSpellType;
    }
}
