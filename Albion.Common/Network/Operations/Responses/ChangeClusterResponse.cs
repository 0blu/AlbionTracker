using Albion.Common.Photon;

namespace Albion.Common.Network.Operations.Responses
{
    [OperationCode(OperationCodes.ChangeCluster)]
    public class ChangeClusterResponse : OperationDataContract
    {
        public string ClusterName;

        public string ClusterMap;

        public string ClusterOwner;
    }
}
