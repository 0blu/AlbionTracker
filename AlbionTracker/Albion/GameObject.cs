using AlbionTracker.WebSocket;
using AlbionTracker.WebSocket.Models;

namespace AlbionTracker.Albion
{
    internal class GameObject
    {
        public enum GameObjectSubType
        {
            Unknown,
            LocalPlayer,
            Player,
            PvpPlayer,
            Mob,
            Boss
        }

        public enum GameObjectType
        {
            Unknown,
            Player,
            Mob
        }

        public readonly long Id;

        public GameObjectType ObjectType = GameObjectType.Unknown;

        public GameObjectSubType ObjectSubType = GameObjectSubType.Unknown;

        public string Name = "Unknown";

        public GameObject(long objectId)
        {
            Id = objectId;
        }

        public override string ToString()
        {
            return $"{ObjectType}[Id: {Id}, Name: '{Name}']";
        }

        public WsMessage GetJoinPackage()
        {
            return new WsNewEntity
            {
                objectId = Id,
                type = ObjectType,
                subType = ObjectSubType,
                name = Name
            };
        }
    }
}
