using Albion.Common.Math;
using Albion.Common.Photon;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.UpdateFame)]
    public class UpdateFame : EventWithObjectId
    {
        public FixPoint Total;
        public FixPoint Change;
        public short GroupSize;
        public bool ClusterBonus;
        public bool PremiumBonus;
        public float BonusFactor;
        public int UsedItemType;
    }
}
