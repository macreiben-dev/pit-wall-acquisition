using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Leadeboards
{
    public sealed class LeaderboardData : ISendableData
    {
        /**
         * This class will holds the data to be sent. Just creating it here to write any ideas about arch.
         * */
        public string SimerKey => throw new NotImplementedException();

        public string PilotName => throw new NotImplementedException();

        public string CarName => throw new NotImplementedException();

        public object AsData()
        {
            throw new NotImplementedException();
        }
    }
}
