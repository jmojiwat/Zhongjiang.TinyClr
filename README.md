# Yf-S201 & Yf-S402 Water Flow Meter library for TinyCLR

This library uses a timer to measure water flow.

```c#
var gpioController = GpioController.GetDefault();
var capability = YfSCapabilityExtensions.Yf201Capability();

var wfm = new YfS(capability, SC20100.GpioPin.PA10, 2000, 1000, gpioController);

while (true)
{
  var flow = wfm.VolumeFlowLitresPerMinute;
  Thread.Sleep(2000);
}
```
