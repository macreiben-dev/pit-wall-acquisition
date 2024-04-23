using System.Collections;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{
    internal sealed class LeadeboardCollection : IEnumerable<ILeaderboardEntry>
    {
        private const int TotalPilotCount = 99;
        private readonly IPluginManagerAdapter _adapter;
        private IList<ILeaderboardEntry> _entries;

        public LeadeboardCollection(IPluginManagerAdapter adapter)
        {
            _adapter = adapter;
        }

        public IEnumerator<ILeaderboardEntry> GetEnumerator()
        {
            if (_entries != null)
            {
                return _entries.GetEnumerator();
            }

            List<ILeaderboardEntry> entries = new List<ILeaderboardEntry>(TotalPilotCount);

            for (int position = 1; position <= 99; position++)
            {
                entries.Add(new LeaderboardEntry(_adapter, position));
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
