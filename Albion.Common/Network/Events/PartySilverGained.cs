using Albion.Common.Math;
using Albion.Common.Photon;
using Albion.Common.Time;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.PartySilverGained)]
    public class PartySilverGained : EventDataContract
    {
        public GameTimeStamp TimeStamp;
        public long TargetEntityId;
        public FixPoint SilverNet;
        public FixPoint SilverPreTax;
    }
}
