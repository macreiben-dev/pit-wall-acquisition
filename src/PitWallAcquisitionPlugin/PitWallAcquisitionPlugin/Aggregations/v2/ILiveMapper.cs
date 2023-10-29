using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin.Aggregations.v2
{
    public interface ILiveMapper
    {
        void Set(IPluginRecordRepository adapter, ILiveAggregator aggregator);
    }
}