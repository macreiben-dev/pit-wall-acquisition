using System.Collections;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{
    internal class LeadeboardCollection : IEnumerable<LeaderboardEntry>
    {
        private IPluginManagerAdapter adapter;
        private IList<LeaderboardEntry> _entries;

        public LeadeboardCollection(IPluginManagerAdapter adapter)
        {
            this.adapter = adapter;
        }

        public IEnumerator<LeaderboardEntry> GetEnumerator()
        {
            if (_entries != null)
            {
                return _entries.GetEnumerator();
            }

            List<LeaderboardEntry> entries = new List<LeaderboardEntry>(99);

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
