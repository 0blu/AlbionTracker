using Albion.Common.Network.Events;
using Albion.Common.Network.Operations.Responses;

namespace AlbionNetworkAnalyzer
{
    public interface IAlbionPackageHandler
    {
        /**
         * Events
         */
        void OnLeave(Leave package);
        void OnHealthUpdate(HealthUpdate package);
        void OnNewCharacter(NewCharacter package);
        void OnTakeSilver(TakeSilver package);
        void OnUpdateFame(UpdateFame package);
        void OnNewMob(NewMob package);
        void OnPartySilverGained(PartySilverGained package);
        void OnInCombatStateUpdate(InCombatStateUpdate package);

        /**
         * Reponses
         */
        void OnChangeClusterResponse(ChangeClusterResponse package);
        void OnJoinResponse(JoinResponse package);
    }
}
