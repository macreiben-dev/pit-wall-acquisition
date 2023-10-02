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
    }
}