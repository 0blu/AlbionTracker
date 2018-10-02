using System;
using Albion.Common.Network;

namespace Albion.Common.Photon
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventCodeAttribute : Attribute
    {
        public EventCodeAttribute(EventCodes iCodes)
        {
            EventCodes = iCodes;
        }

        public EventCodes EventCodes { get; set; }
    }
}
