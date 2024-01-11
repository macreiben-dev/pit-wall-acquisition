using System.Collections;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    internal class Leadeboard : IEnumerable<LeaderboardEntry>
    {
        public IEnumerator<LeaderboardEntry> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
