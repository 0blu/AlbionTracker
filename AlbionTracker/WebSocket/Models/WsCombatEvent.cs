using Albion.Common;

namespace AlbionTracker.WebSocket.Models
{
    class WsCombatEvent : WsMessage
    {
        public long objectId;
        public long timeStamp;
        public float healthChange;
        public float newHealthValue;
        public EffectType effectType;
        public EffectOrigin effectOrigin;
        public long causerId;
        public string causingSpell;

        public WsCombatEvent() : base(MessageType.CombatEvent)
        {
        }
    }
}
