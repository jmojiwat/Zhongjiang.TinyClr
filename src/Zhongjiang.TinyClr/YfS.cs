using System;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;

namespace Zhongjiang.TinyClr
{
    public sealed class YfS : IDisposable
    {
        private readonly YfSCapability capability;
        private readonly GpioPin signalPin;
        private readonly Timer timer;

        private int pulseCount;
        private long ticks;

        public YfS(YfSCapability capability, int signalPinNumber, int initialDelayMilliseconds, int periodMilliseconds,
            GpioController gpioController)
        {
            this.capability = capability;
            signalPin = gpioController.OpenPin(signalPinNumber);
            signalPin.SetDriveMode(GpioPinDriveMode.Input);
            signalPin.ValueChanged += OnPinValueChanged;

            timer = new Timer(OnTimer, null, initialDelayMilliseconds, periodMilliseconds);
        }

        public float MinimumVolumeFlowLitersPerMinute => capability.MinimumVolumeFlowLitresPerMinute;
        public float MaximumVolumeFlowLitersPerMinute => capability.MaximumVolumeFlowLitresPerMinute;


        public float VolumeFlowLitresPerMinute { get; private set; }

        public void Dispose()
        {
            signalPin.Dispose();
            timer.Dispose();
        }

        private void IncrementPulse() => pulseCount++;

        private void OnPinValueChanged(GpioPin sender, GpioPinValueChangedEventArgs e)
        {
            if (e.Edge == GpioPinEdge.RisingEdge)
            {
                IncrementPulse();
            }
        }

        private void OnTimer(object state)
        {
            VolumeFlowLitresPerMinute = ReadVolumeFlowLitresPerMinute();
            ResetCounters();
        }

        private float ReadVolumeFlowLitresPerMinute()
        {
            var frequencyHertz = VolumeFlowMeter.CalculateFrequencyHertz(ticks, DateTime.Now.Ticks, pulseCount);
            return VolumeFlowMeter.CalculateVolumeFlowLitresPerMinute(frequencyHertz, capability.PulsePerLiter);
        }

        private void ResetCounters()
        {
            pulseCount = 0;
            ticks = DateTime.Now.Ticks;
        }
    }
}