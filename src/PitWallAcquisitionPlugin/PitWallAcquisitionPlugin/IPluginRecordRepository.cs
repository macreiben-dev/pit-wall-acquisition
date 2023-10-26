using PitWallAcquisitionPlugin.PluginManagerWrappers;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin
{
    /// <summary>
    /// The wrapper around the plugin manager. This is the primary source of data.
    /// 
    /// The implementation provide conversion into native types.
    /// 
    /// See <see cref="PitWallAcquisitionPlugin.Aggregations.LiveAggregator"/> and <see cref="PitWallAcquisitionPlugin.Aggregations.ILiveAggregator"/> to create the counter aggregations.
    /// </summary>
    public interface IPluginRecordRepository
    {
        /// <summary>
        /// True when game is running.
        /// </summary>
        bool IsGameRunning { get; }

        /// <summary>
        /// Session time left as string
        /// </summary>
        string SessionTimeLeft { get; }

        /// <summary>
        /// The last laptime.
        /// </summary>
        string LastLaptime { get; }
        
        /// <summary>
        /// Tyre wear front left
        /// </summary>
        double? TyreWearFrontLeft { get; }

        /// <summary>
        /// Tyre wear front right
        /// </summary>
        double? TyreWearFrontRight { get; }

        /// <summary>
        /// Tyre wear rear left
        /// </summary>
        double? TyreWearRearLeft { get; }

        /// <summary>
        /// Tyre wear rear right
        /// </summary>
        double? TyreWearRearRight { get; }

        ITyreTemperature TyreFrontLeftTemperature { get; }

        ITyreTemperature TyreFrontRightTemperature { get; }

        ITyreTemperature TyreRearLeftTemperature { get; }

        ITyreTemperature TyreRearRightTemperature { get; }
    }
}