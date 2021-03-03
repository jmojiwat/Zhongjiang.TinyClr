namespace Zhongjiang.TinyClr
{
    public static class YfSCapabilityExtensions
    {
        public static YfSCapability Yf201Capability() =>
            new YfSCapability(450f, 1, 30);

        public static YfSCapability YfS402Capability() =>
            new YfSCapability(4_380, 1, 5);

    }
}