namespace AlbionTracker.WebSocket.Models
{
    class WsChangeCluster : WsMessage
    {
        public string clusterName;

        public WsChangeCluster() : base(MessageType.ChangeCluster)
        {
        }
    }
}
