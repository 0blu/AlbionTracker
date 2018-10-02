namespace AlbionTracker.WebSocket
{
    internal abstract class WsMessage
    {
        public string messageType;

        protected WsMessage(MessageType messageType)
        {
            this.messageType = messageType.ToString();
        }
    }
}
