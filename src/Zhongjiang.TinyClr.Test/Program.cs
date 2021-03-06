﻿using System.Threading;
using DfRobot.TinyClr;
using GHIElectronics.TinyCLR.Devices.Gpio;
using static GHIElectronics.TinyCLR.Pins.SC20100.GpioPin;

namespace Zhongjiang.TinyClr.Test
{
    class Program
    {
        static void Main()
        {
            var lcd = new Eone1602(PC5, PA15, PB1, PA2, PC7, PC6, PC4, GpioController.GetDefault());
            var capability = YfSCapabilityExtensions.Yf201Capability();

            var gpioController = GpioController.GetDefault();
            var wfm = new YfS(capability, PA10, 2000, 1000, gpioController);
            var wfm2 = new YfS(capability, PA9, 2000, 1000, gpioController);
            var wfm3 = new YfS(capability, PA1, 2000, 1000, gpioController);
            var wfm4 = new YfS(capability, PB0, 2000, 1000, gpioController);
            var wfm5 = new YfS(capability, PA7, 2000, 1000, gpioController);

            while (true)
            {
                lcd.Clear();
                lcd.Print($"Flow: {wfm.VolumeFlowLitresPerMinute} L/m");
                lcd.SetCursor(0, 1);
                lcd.Print(GHIElectronics.TinyCLR.Native.Memory.ManagedMemory.FreeBytes.ToString());
                Thread.Sleep(2000);
            }
        }
    }
}
