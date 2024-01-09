using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Leadeboards
{
    public sealed class FakeLeaderboardLiveAggregator : ILeaderboardLiveAggregator
    {
        public bool IsDirty => true;

        public ISendableData AsData()
        {
            return new DummyData()
            {
                CarName = string.Empty,
                PilotName = string.Empty,
                SimerKey = string.Empty,
            };
        }
        public void Clear()
        {
            // Do nothing here.
        }

        public void UpdateAggregator(IPluginRecordRepository racingDataRepository)
        {
            // Do nothing here.
        }
    }
}
