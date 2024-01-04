using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{
    public interface ILiveMapper
    {
        void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator);
    }
}