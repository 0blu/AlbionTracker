using System;
using Albion.Common.GameData;
using Albion.Common.GameData.World;

namespace AlbionTracker.Albion
{
    internal class StateHandler
    {
        public readonly EntityManager EntityManager = new EntityManager();
        public readonly FarmManager FarmManager = new FarmManager();

        public ClusterInfo CurrentCluster
        {
            get;
            private set;
        }

        public string ClusterOwner
        {
            get;
            private set;
        }

        protected readonly GameData GameData;
        private string _lastClusterHash;

        public StateHandler(GameData gameData)
        {
            GameData = gameData;
        }

        private bool TryChangeCluster(string name, string mapName, string clusterOwner)
        {
            string newClusterHash = string.Empty + name + mapName + clusterOwner;

            if (_lastClusterHash == newClusterHash)
                return false;

            _lastClusterHash = newClusterHash;

            return true;
        }

        public void SetNewCluster(string name, string mapName, string clusterOwner)
        {
            if (!TryChangeCluster(name, mapName, clusterOwner))
                return;
            
            if (string.IsNullOrEmpty(mapName))
                mapName = name;

            CurrentCluster = GameData.World.GetClusterByName(mapName);
            ClusterOwner = clusterOwner;

            EntityManager.RemoveAll();
            Console.WriteLine($"[StateHandler] Changed cluster to: '{CurrentCluster.Name}' ArcheType: '{CurrentCluster.ClusterType.ArcheType.Name}'");
            OnChangeCluster?.Invoke(CurrentCluster);
        }

        public void UpdateCombatMode(bool inActiveCombat, bool inPassiveCombat)
        {
            OnChangeCombatMode?.Invoke(inActiveCombat, inPassiveCombat);
        }

        public event Action<ClusterInfo> OnChangeCluster;
        public event Action<bool, bool> OnChangeCombatMode;
    }
}
