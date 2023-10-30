using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Mappers
{
    public interface ILiveMapper
    {
        void Set(IPluginRecordRepository adapter, ILiveAggregator aggregator);
    }
}