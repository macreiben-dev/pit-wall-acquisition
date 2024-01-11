using System.Collections;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{
    internal sealed class LeadeboardCollection : IEnumerable<ILeaderboardEntry>
    {
        private const int TOTAL_PILOT_COUNT = 99;
        private IPluginManagerAdapter adapter;
        private IList<ILeaderboardEntry> _entries;

        public LeadeboardCollection(IPluginManagerAdapter adapter)
        {
            this.adapter = adapter;
        }

        public IEnumerator<ILeaderboardEntry> GetEnumerator()
        {
            if (_entries != null)
            {
                return _entries.GetEnumerator();
            }

            List<ILeaderboardEntry> entries = new List<ILeaderboardEntry>(TOTAL_PILOT_COUNT);

            for (int position = 1; position <= 99; position++)
            {
                entries.Add(new LeaderboardEntry(adapter, position));
            }

            _entries = entries;

            return _entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
