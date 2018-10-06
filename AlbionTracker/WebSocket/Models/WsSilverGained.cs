namespace AlbionTracker.WebSocket.Models
{
    class WsSilverGained : WsMessage
    {
        public long objectId;
        public double amount;

        public WsSilverGained() : base(MessageType.SilverGained)
        {
        }
    }
}
