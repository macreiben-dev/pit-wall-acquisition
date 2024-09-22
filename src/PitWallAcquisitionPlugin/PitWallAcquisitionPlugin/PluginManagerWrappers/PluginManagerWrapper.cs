using PitWallAcquisitionPlugin.PluginManagerWrappers.Telemetries;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    /// <summary>
    /// The wrapper around the plugin manager. This is the primary source of data.
    /// </summary>
    internal sealed class PluginManagerWrapper : IPluginRecordRepository
    {
        private static class Constants
        {
            public const string TyreTemperatureFrontLeftInner = "DataCorePlugin.GameData.TyreTemperatureFrontLeftInner";
            public const string TyreTemperatureFrontLeftMiddle = "DataCorePlugin.GameData.TyreTemperatureFrontLeftMiddle";
            public const string TyreTemperatureFrontLeftOuter = "DataCorePlugin.GameData.TyreTemperatureFrontLeftOuter";
            public const string TyreTemperatureFrontLeft = "DataCorePlugin.GameData.TyreTemperatureFrontLeft";

            public const string TyreTemperatureFrontRightInner = "DataCorePlugin.GameData.TyreTemperatureFrontRightInner";
            public const string TyreTemperatureFrontRightMiddle = "DataCorePlugin.GameData.TyreTemperatureFrontRightMiddle";
            public const string TyreTemperatureFrontRightOuter = "DataCorePlugin.GameData.TyreTemperatureFrontRightOuter";
            public const string TyreTemperatureFrontRight = "DataCorePlugin.GameData.TyreTemperatureFrontRight";

            public const string TyreTemperatureRearLeftInner = "DataCorePlugin.GameData.TyreTemperatureRearLeftInner";
            public const string TyreTemperatureRearLeftMiddle = "DataCorePlugin.GameData.TyreTemperatureRearLeftMiddle";
            public const string TyreTemperatureRearLeftOuter = "DataCorePlugin.GameData.TyreTemperatureRearLeftOuter";
            public const string TyreTemperatureRearLeft = "DataCorePlugin.GameData.TyreTemperatureRearLeft";

            public const string TyreTemperatureRearRightInner = "DataCorePlugin.GameData.TyreTemperatureRearRightInner";
            public const string TyreTemperatureRearRightMiddle = "DataCorePlugin.GameData.TyreTemperatureRearRightMiddle";
            public const string TyreTemperatureRearRightOuter = "DataCorePlugin.GameData.TyreTemperatureRearRightOuter";
            public const string TyreTemperatureRearRight = "DataCorePlugin.GameData.TyreTemperatureRearRight";

            public const string TyreWearFrontLeft = "DataCorePlugin.GameData.TyreWearFrontLeft";
            public const string TyreWearFrontRight = "DataCorePlugin.GameData.TyreWearFrontRight";
            public const string TyreWearRearLeft = "DataCorePlugin.GameData.TyreWearRearLeft";
            public const string TyreWearRearRight = "DataCorePlugin.GameData.TyreWearRearRight";

            public const string GameRunning = "DataCorePlugin.GameRunning";

            public const string SessionTimeLeft = "DataCorePlugin.GameData.SessionTimeLeft";
            public const string LastLapTime = "DataCorePlugin.GameData.LastLapTime";

            public const string AvgPathWetness = "DataCorePlugin.GameRawData.Scoring.mScoringInfo.mAvgPathWetness";
            public const string Raining = "GameRawData.Scoring.mScoringInfo.mRaining";
            public const string AirTemperature = "DataCorePlugin.GameData.AirTemperature";
            public const string TrackTemperature = "DataCorePlugin.GameData.RoadTemperature";

            public const string Fuel = "DataCorePlugin.GameData.Fuel";
            public const string MaxFuel = "DataCorePlugin.GameData.MaxFuel";
            public const string ComputedLastLapConsumption = "DataCorePlugin.Computed.Fuel_LastLapConsumption";
            public const string ComputedLiterPerLaps = "DataCorePlugin.Computed.Fuel_LitersPerLap";
            public const string ComputedRemainingLaps = "DataCorePlugin.Computed.Fuel_RemainingLaps";
            public const string ComputedRemainingTime = "DataCorePlugin.Computed.Fuel_RemainingTime";
        }

        private readonly IPluginManagerAdapter _pluginManager;

        public PluginManagerWrapper(IPluginManagerAdapter pluginManager)
        {
            _pluginManager = pluginManager;

            TyreFrontLeftTemperature = new TyreTemperature(
                Constants.TyreTemperatureFrontLeftInner,
                Constants.TyreTemperatureFrontLeftMiddle,
                Constants.TyreTemperatureFrontLeftOuter,
                Constants.TyreTemperatureFrontLeft,
                _pluginManager);

            TyreFrontRightTemperature = new TyreTemperature(
                Constants.TyreTemperatureFrontRightInner,
                Constants.TyreTemperatureFrontRightMiddle,
                Constants.TyreTemperatureFrontRightOuter,
                Constants.TyreTemperatureFrontRight,
                _pluginManager);

            TyreRearLeftTemperature = new TyreTemperature(
                Constants.TyreTemperatureRearLeftInner,
                Constants.TyreTemperatureRearLeftMiddle,
                Constants.TyreTemperatureRearLeftOuter,
                Constants.TyreTemperatureRearLeft,
                _pluginManager);

            TyreRearRightTemperature = new TyreTemperature(
                Constants.TyreTemperatureRearRightInner,
                Constants.TyreTemperatureRearRightMiddle,
                Constants.TyreTemperatureRearRightOuter,
                Constants.TyreTemperatureRearRight,
                _pluginManager);
        }

        public ITyreTemperature TyreFrontLeftTemperature { get; }

        public ITyreTemperature TyreFrontRightTemperature { get; }

        public ITyreTemperature TyreRearLeftTemperature { get; }

        public ITyreTemperature TyreRearRightTemperature { get; }

        public bool IsGameRunning =>
          PluginManagerFieldConverter.ToBoolean(
              Constants.GameRunning, _pluginManager);

        public string SessionTimeLeft => PluginManagerFieldConverter.ToString(
            Constants.SessionTimeLeft, _pluginManager);

        public string LastLaptime => PluginManagerFieldConverter.ToString(
            Constants.LastLapTime, _pluginManager);

        public double? TyreWearFrontLeft => PluginManagerFieldConverter.ToDouble(
            Constants.TyreWearFrontLeft, _pluginManager);

        public double? TyreWearFrontRight => PluginManagerFieldConverter.ToDouble(
            Constants.TyreWearFrontRight, _pluginManager);

        public double? TyreWearRearLeft => PluginManagerFieldConverter.ToDouble(
            Constants.TyreWearRearLeft, _pluginManager);

        public double? TyreWearRearRight => PluginManagerFieldConverter.ToDouble(
            Constants.TyreWearRearRight, _pluginManager);

        public double? AvgRoadWetness => PluginManagerFieldConverter.ToDouble(
            Constants.AvgPathWetness, _pluginManager);

        public double? Raining => PluginManagerFieldConverter.ToDouble(
            Constants.Raining, _pluginManager);
        public double? AirTemperature => PluginManagerFieldConverter.ToDouble(
            Constants.AirTemperature, _pluginManager);

        public double? TraceTemperature => PluginManagerFieldConverter.ToDouble(
            Constants.TrackTemperature, _pluginManager);

        public double? Fuel => PluginManagerFieldConverter.ToDouble(
            Constants.Fuel, _pluginManager);

        public double? MaxFuel => PluginManagerFieldConverter.ToDouble(
            Constants.MaxFuel, _pluginManager);

        public double? ComputedLastLapConsumption => PluginManagerFieldConverter.ToDouble(
            Constants.ComputedLastLapConsumption, _pluginManager);

        public double? ComputedLiterPerLaps => PluginManagerFieldConverter.ToDouble(
            Constants.ComputedLiterPerLaps, _pluginManager);

        public double? ComputedRemainingLaps => PluginManagerFieldConverter.ToDouble(
            Constants.ComputedRemainingLaps, _pluginManager);

        public string ComputedRemainingTime => PluginManagerFieldConverter.ToString(
            Constants.ComputedRemainingTime, _pluginManager);

        public IPluginManagerAdapter PluginManager => _pluginManager;
    }
}
