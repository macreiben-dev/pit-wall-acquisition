using PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin.Acquisition.Telemetries.Mappers
{
    internal interface ILiveTelemetryMapper
    {
        void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator);
    }
}