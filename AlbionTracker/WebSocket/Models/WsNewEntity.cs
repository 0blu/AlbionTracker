using AlbionTracker.Albion;

namespace AlbionTracker.WebSocket.Models
{
    class WsNewEntity : WsMessage
    {
        public long objectId;
        public GameObject.GameObjectType type;
        public GameObject.GameObjectSubType subType;
        public string name;

        public WsNewEntity() : base(MessageType.NewEntity)
        {
        }
    }
}
