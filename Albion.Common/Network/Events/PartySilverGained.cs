using Albion.Common.Math;
using Albion.Common.Photon;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.PartySilverGained)]
    public class PartySilverGained : EventDataContract
    {
        public long TimeStamp;
        public long TargetEntityId;
        public FixPoint SilverNet;
        public FixPoint SilverPreTax;
    }
}
