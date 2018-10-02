using Albion.Common.Photon;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.InCombatStateUpdate)]
    public class InCombatStateUpdate : EventDataContract
    {
        public bool InActiveCombat;

        public bool InPassiveCombat;
    }
}
