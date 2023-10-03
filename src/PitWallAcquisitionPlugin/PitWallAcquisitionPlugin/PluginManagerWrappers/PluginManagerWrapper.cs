using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    /// <summary>
    /// The wrapper around the plugin manager. This is the primary source of data.
    /// </summary>
    public sealed class PluginManagerWrapper : IPluginRecordRepository
    {
        private readonly IPluginManagerAdapter _pluginManager;
        private readonly bool _isGameRunning;
        private readonly string _sessionTimeLeft;

        public PluginManagerWrapper(IPluginManagerAdapter pluginManager)
        {
            _pluginManager = pluginManager;
        }

        // ==================================================

        public bool IsGameRunning =>
          PluginManagerFieldConverter.ToBoolean("DataCorePlugin.GameRunning", _pluginManager);

        public string SessionTimeLeft => PluginManagerFieldConverter.ToString("DataCorePlugin.GameData.SessionTimeLeft", _pluginManager);

        public string LastLaptime => PluginManagerFieldConverter.ToString("DataCorePlugin.GameData.LastLapTime", _pluginManager);

        public double? TyreWearFrontLeft => PluginManagerFieldConverter.ToDouble("DataCorePlugin.GameData.TyreWearFrontLeft", _pluginManager);

        public double? TyreWearFrontRight => PluginManagerFieldConverter.ToDouble("DataCorePlugin.GameData.TyreWearFrontRight", _pluginManager);

        public double? TyreWearRearLeft => PluginManagerFieldConverter.ToDouble("DataCorePlugin.GameData.TyreWearRearLeft", _pluginManager);

        public double? TyreWearRearRight => PluginManagerFieldConverter.ToDouble("DataCorePlugin.GameData.TyreWearRearRight", _pluginManager);

        // TODO pilot name is hard coded for the moment. It will be set from sh GUI
        public string DriverName => "Pilot1";
    }
}
