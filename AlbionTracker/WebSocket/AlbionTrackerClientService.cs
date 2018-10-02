using AlbionTracker.Albion;
using AlbionTracker.WebSocket.Models;
using Newtonsoft.Json;
using WebSocketSharp.Server;

namespace AlbionTracker.WebSocket
{
    internal class AlbionTrackerClientService : WebSocketBehavior
    {
        private readonly StateHandler _stateHandler;

        public AlbionTrackerClientService(StateHandler stateHandler)
        {
            _stateHandler = stateHandler;
        }

        protected override void OnOpen()
        {
            Send(JsonConvert.SerializeObject(new WsChangeCluster
            {
                clusterName = _stateHandler.CurrentCluster?.DisplayName,
            }));

            foreach (var gameEntity in _stateHandler.EntityManager.GetEntities())
            {
                Send(JsonConvert.SerializeObject(gameEntity.GetJoinPackage()));
            }

            base.OnOpen();
        }
    }
}
