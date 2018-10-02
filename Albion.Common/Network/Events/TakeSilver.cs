using Albion.Common.Math;
using Albion.Common.Photon;
using Albion.Common.Time;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.TakeSilver)]
    public class TakeSilver : EventWithObjectId
    {
        public GameTimeStamp TimeStamp;
        public long TargetEntityId;
        public FixPoint YieldPreTax;
        public FixPoint ClusterTax;
        public FixPoint GuildTax;
        public bool PremiumBonus;
        public bool ClusterBonus;
    }
}
