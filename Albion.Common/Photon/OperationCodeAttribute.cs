using System;
using Albion.Common.Network;

namespace Albion.Common.Photon
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OperationCodeAttribute : Attribute
    {
        public OperationCodeAttribute(OperationCodes operationCode)
        {
            OperationCode = operationCode;
        }

        public OperationCodes OperationCode { get; set; }
    }
}
