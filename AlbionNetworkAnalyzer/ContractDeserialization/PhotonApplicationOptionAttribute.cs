using System;

namespace AlbionNetworkAnalyzer.ContractDeserialization
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PhotonApplicationOptionAttribute : Attribute
    {
        public Type PeerType { get; }

        public PhotonApplicationOptionAttribute(Type peerType)
        {
            PeerType = peerType;
        }
    }
}
