using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers;
using System;

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
            ToBoolean("DataCorePlugin.GameRunning", _pluginManager);

        public string SessionTimeLeft =>
            ToString("DataCorePlugin.GameData.SessionTimeLeft", _pluginManager);

        public string LastLaptime => ToString("DataCorePlugin.GameData.LastLapTime", _pluginManager);

        public double? TyreWearFrontLeft => ToDouble("DataCorePlugin.GameData.TyreWearFrontLeft", _pluginManager);

        public double? TyreWearFrontRight => ToDouble("DataCorePlugin.GameData.TyreWearFrontRight", _pluginManager);

        public double? TyreWearRearLeft => ToDouble("DataCorePlugin.GameData.TyreWearRearLeft", _pluginManager);

        // TODO pilot name is hard coded for the moment. It will be set from sh GUI
        public string DriverName => "Pilot1";


        // ==================================================

        private double? ToDouble(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return null;
            }

            return Convert.ToDouble(data);
        }

        private string ToString(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return null;
            }

            return data.ToString();
        }

        private bool ToBoolean(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return false;
            }

            return Convert.ToBoolean(data);
        }
    }
}
