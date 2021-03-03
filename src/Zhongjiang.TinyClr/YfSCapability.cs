namespace Zhongjiang.TinyClr
{
    public class YfSCapability
    {
        public YfSCapability(float pulsePerLiter, float minimumVolumeFlowLitresPerMinute, float maximumVolumeFlowLitresPerMinute)
        {
            PulsePerLiter = pulsePerLiter;
            MinimumVolumeFlowLitresPerMinute = minimumVolumeFlowLitresPerMinute;
            MaximumVolumeFlowLitresPerMinute = maximumVolumeFlowLitresPerMinute;
        }

        public float PulsePerLiter { get; }
        public float MinimumVolumeFlowLitresPerMinute { get; }
        public float MaximumVolumeFlowLitresPerMinute { get; }
    }
}