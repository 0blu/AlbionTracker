using Albion.Common.Photon;

namespace Albion.Common.Network
{
    public abstract class OperationDataContract
    {
        [DataField(FieldIndex = 253)]
        public OperationCodes OpCode;

        [DataField(FieldIndex = 255)]
        public int MessageId;
    }
}
