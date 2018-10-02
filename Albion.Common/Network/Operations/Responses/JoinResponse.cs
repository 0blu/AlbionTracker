using Albion.Common.Photon;
using System;

namespace Albion.Common.Network.Operations.Responses
{
    [OperationCode(OperationCodes.Join)]
    public class JoinResponse
    {
        public long CharacterId;

        public Guid CharacterGuid;

        public string CharacterName;
    }
}
