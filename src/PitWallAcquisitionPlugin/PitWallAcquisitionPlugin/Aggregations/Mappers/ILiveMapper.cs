using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin.Aggregations.Mappers
{
    public interface ILiveMapper
    {
        void Set(IPluginRecordRepository adapter, ILiveAggregator aggregator);
    }
}