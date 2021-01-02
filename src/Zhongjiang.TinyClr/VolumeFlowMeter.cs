using System;

namespace Zhongjiang.TinyClr
{
    public static class VolumeFlowMeter
    {
        public static float CalculateFrequencyHertz(long startTicks, long endTicks, long pulseCount) => 
            (float) (pulseCount / TimeSpan.FromTicks(endTicks - startTicks).TotalSeconds);

        public static float CalculateVolumeFlowLitresPerMinute(float pulseFrequencyHertz, double pulsePerLiterFactor) => 
            (float)(60 * pulseFrequencyHertz / pulsePerLiterFactor);
    }
}