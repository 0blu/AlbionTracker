using System;
using System.Net;
using Albion.Common;
using Albion.Common.GameData;
using Albion.Common.GameData.World;
using Albion.Common.Math;
using Albion.Common.Time;
using AlbionTracker.Albion;
using AlbionTracker.WebSocket.Models;
using Newtonsoft.Json;
using WebSocketSharp.Server;

namespace AlbionTracker.WebSocket
{
    internal class SocketServer : IDisposable
    {
        private readonly WebSocketServer _wss;

        private readonly StateHandler _stateHandler;
        private readonly GameData _gameData;

        public SocketServer(StateHandler stateHandler, GameData gameData)
        {
            _gameData = gameData;
            _stateHandler = stateHandler;

            RegisterEvents();

            _wss = new WebSocketServer(IPAddress.Loopback, 4537);
            _wss.AddWebSocketService("/ws", () => new AlbionTrackerClientService(_stateHandler));
        }

        public void Start()
        {
            _wss.Start();
        }

        private void RegisterEvents()
        {
            _stateHandler.OnChangeCluster += SendChangeCluster;
            _stateHandler.OnChangeCombatMode += SendCombatMode;
            _stateHandler.FarmManager.OnGainedSilver += SendGainedSilver;
            _stateHandler.EntityManager.OnAddEntitiy += SendNewEntity;
            _stateHandler.EntityManager.OnHealthUpdate += SendHealthUpdate;
        }

        private void UnregisterEvents()
        {
            _stateHandler.EntityManager.OnHealthUpdate -= SendHealthUpdate;
            _stateHandler.EntityManager.OnAddEntitiy -= SendNewEntity;
            _stateHandler.FarmManager.OnGainedSilver -= SendGainedSilver;
            _stateHandler.OnChangeCombatMode -= SendCombatMode;
            _stateHandler.OnChangeCluster -= SendChangeCluster;
        }

        public void Dispose()
        {
            UnregisterEvents();
            _wss?.Stop();
        }

        private void SendGainedSilver(long objectId, FixPoint amount)
        {
            Broadcast(new WsSilverGained
            {
                objectId = objectId,
                amount = amount.FloatValue,
            });
        }

        public void SendHealthUpdate(long objectId, GameTimeStamp timeStamp, float healthChange, float newHealthValue, EffectType effectType, EffectOrigin effectOrigin, long causerId, int causingSpellType)
        {
            Broadcast(new WsCombatEvent
            {
                objectId = objectId,
                timeStamp = timeStamp.Ticks,
                healthChange = healthChange,
                newHealthValue = newHealthValue,
                effectType = effectType,
                effectOrigin = effectOrigin,
                causerId = causerId,
                causingSpell = _gameData.SpellData.GetByType(causingSpellType)?.Name
            });
        }


        public void SendNewEntity(GameObject gameEntity)
        {
            Broadcast(gameEntity.GetJoinPackage());
        }

        public void SendChangeCluster(ClusterInfo cluster)
        {
            Broadcast(new WsChangeCluster { clusterName = cluster.DisplayName });
        }

        public void SendCombatMode(bool inActiveCombat, bool inPassiveCombat)
        {
            Broadcast(new WsInCombatStateUpdate
            {
                inActiveCombat = inActiveCombat,
                inPassiveCombat = inPassiveCombat,
            });
        }

        private void Broadcast(WsMessage wsMessage)
        {
            _wss.WebSocketServices.Broadcast(JsonConvert.SerializeObject(wsMessage));
        }
    }
}
