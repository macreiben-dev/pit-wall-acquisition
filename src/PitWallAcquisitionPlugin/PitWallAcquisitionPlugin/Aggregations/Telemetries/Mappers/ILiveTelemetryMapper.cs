using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{
    public interface ILiveTelemetryMapper
    {
        void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator);
    }
}