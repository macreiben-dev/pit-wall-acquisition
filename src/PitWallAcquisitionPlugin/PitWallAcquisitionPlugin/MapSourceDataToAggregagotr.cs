using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Aggregations.v2;

namespace PitWallAcquisitionPlugin
{
    public static class MapSourceDataToAggregagtor
    {
        public static void UpdateAggregatorNow(
            ILiveAggregator aggregator, 
            IPluginRecordRepository racingDataRepository, 
            IMappingConfigurationRepository mappingConfiguration)
        {
            foreach (var config in mappingConfiguration)
            {
                config.Set(racingDataRepository, aggregator);
            }
        }
    }
}