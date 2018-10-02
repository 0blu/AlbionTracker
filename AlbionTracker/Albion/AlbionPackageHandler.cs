using Albion.Common.GameData;
using Albion.Common.Network.Events;
using Albion.Common.Network.Operations.Responses;
using AlbionNetworkAnalyzer;

namespace AlbionTracker.Albion
{
    internal class AlbionPackageHandler : IAlbionPackageHandler
    {
        private readonly StateHandler _stateHandler;
        private readonly GameData _gameData;

        public AlbionPackageHandler(StateHandler stateHandler, GameData gameData)
        {
            _stateHandler = stateHandler;
            _gameData = gameData;
        }

        public void OnLeave(Leave package)
        {
        }

        public void OnInCombatStateUpdate(InCombatStateUpdate package)
        {
            _stateHandler.UpdateCombatMode(package.InActiveCombat, package.InPassiveCombat);
        }

        public void OnChangeClusterResponse(ChangeClusterResponse package)
        {
            _stateHandler.SetNewCluster(package.ClusterName, package.ClusterMap, package.ClusterOwner);
        }

        public void OnJoinResponse(JoinResponse package)
        {
            _stateHandler.EntityManager.AddEntity(package.CharacterId, package.CharacterName, GameObject.GameObjectType.Player, GameObject.GameObjectSubType.LocalPlayer);
        }

        public void OnHealthUpdate(HealthUpdate package)
        {
            _stateHandler.EntityManager.HealthUpdate(
                package.ObjectId,
                package.TimeStamp,
                package.HealthChange,
                package.NewHealthValue,
                package.EffectType,
                package.EffectOrigin,
                package.CauserId,
                package.CausingSpellType
                );
        }

        public void OnNewMob(NewMob package)
        {
            var mobInfo = _gameData.MobData.GetByType(package.Type);
            bool isBossMob = mobInfo.AvatarRing == "RING_MOB_BOSS";

            _stateHandler.EntityManager.AddEntity(
                package.ObjectId,
                mobInfo.Name,
                GameObject.GameObjectType.Mob,
                isBossMob ? GameObject.GameObjectSubType.Boss : GameObject.GameObjectSubType.Mob
            );
        }

        public void OnNewCharacter(NewCharacter package)
        {
            _stateHandler.EntityManager.AddEntity(package.ObjectId, package.CharacterName, GameObject.GameObjectType.Player, GameObject.GameObjectSubType.Player);
        }

        public void OnUpdateFame(UpdateFame package)
        {
        }

        public void OnPartySilverGained(PartySilverGained package)
        {
        }

        public void OnTakeSilver(TakeSilver package)
        {
        }
    }
}
