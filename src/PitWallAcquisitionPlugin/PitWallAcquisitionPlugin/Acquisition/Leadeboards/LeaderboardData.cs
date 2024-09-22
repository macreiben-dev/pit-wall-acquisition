using System.Collections.Generic;
using PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards;

namespace PitWallAcquisitionPlugin.Acquisition.Leadeboards
{
    internal sealed class LeaderboardData : ISendableData
    {
        /**
         * This class will holds the data to be sent. Just creating it here to write any ideas about arch.
         * */
        public string SimerKey { get; set; }

        public string PilotName { get; set; }

        public string CarName { get; set; }
        public List<ILeaderboardEntry> Entries { get; internal set; }

        public object AsData()
        {
            return this;
        }
    }
}
