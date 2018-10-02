using Albion.Common.Photon;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.NewMob)]
    public class NewMob : EventWithObjectId
    {
        public short Type;
    }
}
