﻿namespace PitWallAcquisitionPlugin.Acquisition.Leadeboards
{
    internal sealed class FakeLeaderboardLiveAggregator : ILeaderboardLiveAggregator
    {
        public bool IsDirty => true;

        public ISendableData AsData()
        {
            return new LeaderboardData()
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
