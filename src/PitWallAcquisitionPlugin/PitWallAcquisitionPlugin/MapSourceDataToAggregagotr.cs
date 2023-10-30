using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations;

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