﻿using System;
using System.Threading;
using GHIElectronics.TinyCLR.Devices.Gpio;
using static Zhongjiang.TinyClr.VolumeFlowMeter;

namespace Zhongjiang.TinyClr
{
    public sealed class YfS201 : IDisposable
    {
        private const float PulsePerLitre = 450f;
        private readonly GpioPin signalPin;
        private readonly Timer timer;

        private int pulseCount;
        private long ticks;

        public YfS201(int signalPinNumber, int initialDelayMilliseconds, int periodMilliseconds,
            GpioController gpioController)
        {
            signalPin = gpioController.OpenPin(signalPinNumber);
            signalPin.SetDriveMode(GpioPinDriveMode.Input);
            signalPin.ValueChanged += OnPinValueChanged;

            timer = new Timer(OnTimer, null, initialDelayMilliseconds, periodMilliseconds);
        }


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
            var frequencyHertz = CalculateFrequencyHertz(ticks, DateTime.Now.Ticks, pulseCount);
            return CalculateVolumeFlowLitresPerMinute(frequencyHertz, PulsePerLitre);
        }

        private void ResetCounters()
        {
            pulseCount = 0;
            ticks = DateTime.Now.Ticks;
        }
    }
}