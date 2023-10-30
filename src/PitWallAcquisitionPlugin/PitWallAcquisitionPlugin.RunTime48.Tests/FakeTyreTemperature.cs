using PitWallAcquisitionPlugin.PluginManagerWrappers;

namespace PitWallAcquisitionPlugin.RunTime48.Tests
{
    public class FakeTyreTemperature : ITyreTemperature
    {
        public double? Average { get; set; }

        public double? Inner { get; set; }

        public double? Middle { get; set; }

        public double? Outer { get; set; }
    }
}

