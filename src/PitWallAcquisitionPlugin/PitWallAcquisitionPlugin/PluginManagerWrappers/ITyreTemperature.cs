namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    internal interface ITyreTemperature
    {
        double? Average { get; }
        double? Inner { get; }
        double? Middle { get; }
        double? Outer { get; }
    }
}
