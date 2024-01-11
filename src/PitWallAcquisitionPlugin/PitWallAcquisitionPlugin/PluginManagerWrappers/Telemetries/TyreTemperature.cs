namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Telemetries
{

    internal sealed class TyreTemperature : ITyreTemperature
    {
        private readonly string innerKey;
        private readonly string middleKey;
        private readonly string outerKey;
        private readonly string averageKey;
        private readonly IPluginManagerAdapter pluginManager;

        public TyreTemperature(
            string innerKey,
            string middleKey,
            string outerKey,
            string average,
            IPluginManagerAdapter pluginManager)
        {
            this.innerKey = innerKey;
            this.middleKey = middleKey;
            this.outerKey = outerKey;
            averageKey = average;

            this.pluginManager = pluginManager;
        }

        public double? Average => PluginManagerFieldConverter.ToDouble(averageKey, pluginManager);

        public double? Inner => PluginManagerFieldConverter.ToDouble(innerKey, pluginManager);

        public double? Middle => PluginManagerFieldConverter.ToDouble(middleKey, pluginManager);

        public double? Outer => PluginManagerFieldConverter.ToDouble(outerKey, pluginManager);
    }
}
