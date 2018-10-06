using Albion.Common.Math;
using System;

namespace AlbionTracker.Albion
{
    internal class FarmManager
    {
        public void GainedSilver(long entityId, FixPoint silverNet)
        {
            OnGainedSilver?.Invoke(entityId, silverNet);
        }

        public event Action<long, FixPoint> OnGainedSilver;
    }
}
