using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Leadeboards
{
    internal sealed class LeaderboardData : ISendableData
    {
        /**
         * This class will holds the data to be sent. Just creating it here to write any ideas about arch.
         * */
        public string SimerKey { get; set; }

        public string PilotName { get; set; }

        public string CarName { get; set; }

        public object AsData()
        {
            throw new NotImplementedException();
        }
    }
}
