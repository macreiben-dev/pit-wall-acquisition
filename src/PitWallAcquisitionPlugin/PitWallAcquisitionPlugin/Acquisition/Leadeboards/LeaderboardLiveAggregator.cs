using System.Collections.Generic;
using System.Linq;
using PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.Acquisition.Leadeboards
{
    internal sealed class LeaderboardLiveAggregator : ILeaderboardLiveAggregator
    {
        private readonly IPitWallConfiguration configuration;
        private IEnumerable<ILeaderboardEntry> _entries;

        public bool IsDirty => true;

        public LeaderboardLiveAggregator(IPitWallConfiguration configuration)
        {
            this.configuration = configuration;
            _entries = Enumerable.Empty<ILeaderboardEntry>();
        }

        public ISendableData AsData()
        {
            return new LeaderboardData()
            {
                CarName = configuration.CarName,
                PilotName = configuration.PilotName,
                SimerKey = configuration.PersonalKey,
                Entries = _entries.ToList()
            };
        }
        

        public void Clear()
        {
            // Do nothing here.
        }

        public void UpdateAggregator(IPluginRecordRepository racingDataRepository)
        {
            /**
             * Have to use the the plugin manager directly.
             * 
             * Will rework the method once something is pushed to the server.
             * */
            _entries = new LeadeboardCollection(racingDataRepository.PluginManager);
        }
    }
}
