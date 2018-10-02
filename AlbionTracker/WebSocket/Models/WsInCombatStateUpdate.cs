namespace AlbionTracker.WebSocket.Models
{
    class WsInCombatStateUpdate : WsMessage
    {
        public bool inActiveCombat;
        public bool inPassiveCombat;

        public WsInCombatStateUpdate() : base(MessageType.InCombatStateUpdate)
        {
        }
    }
}
