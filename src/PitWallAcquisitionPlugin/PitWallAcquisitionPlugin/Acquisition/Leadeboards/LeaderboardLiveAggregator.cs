using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using System;
using System.Net.Sockets;

namespace PitWallAcquisitionPlugin.Aggregations.Leadeboards
{
    public class LeaderboardLiveAggregator : ILeaderboardLiveAggregator
    {
        public bool IsDirty => throw new NotImplementedException();

        public ISendableData AsData()
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void UpdateAggregator(IPluginRecordRepository racingDataRepository)
        {
            throw new NotImplementedException();
        }
    }
}
