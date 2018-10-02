using Albion.Common.Photon;

namespace Albion.Common.Network.Events
{
    [EventCode(EventCodes.NewCharacter)]
    public class NewCharacter : EventWithObjectId
    {
        public string CharacterName;
    }
}
