using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Leadeboards
{
    internal interface IAggregator
    {
        bool IsDirty { get; }

        ISendableData AsData();

        void Clear();

        void UpdateAggregator(IPluginRecordRepository racingDataRepository);
    }
}
