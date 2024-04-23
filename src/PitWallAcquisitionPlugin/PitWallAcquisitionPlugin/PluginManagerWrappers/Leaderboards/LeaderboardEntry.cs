using System;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{

    internal sealed class LeaderboardEntry : ILeaderboardEntry
    {
        private readonly IPluginManagerAdapter pluginAdapter;
        private readonly int position;
        private readonly double _lastlapInSeconds;
        private readonly string _carName;
        private readonly string _carNumber;
        private readonly bool _inPitLane;

        public LeaderboardEntry(IPluginManagerAdapter pluginAdapter, int position)
        {
            this.pluginAdapter = pluginAdapter;
            this.position = position;

            _lastlapInSeconds = ReadDouble("LastLap");
            _carName = ReadString("CarName");
            _carNumber = ReadString("CarNumber");
            _inPitLane = ReadBool("InPitLane");
        }

        private bool ReadBool(string metricName)
        {
            var actual = pluginAdapter.GetPropertyValue(
                BuildMetricName(metricName));

            var intermediary = actual is int ? (int)actual : 0;
            
            if (intermediary == 1)
            {
                return true;
            }
            
            return false;
        }

        private const string Prefix = "GarySwallowDataPlugin.Leaderboard.Position{0:00}.{1}";

        public double LastLapInSeconds => _lastlapInSeconds;

        public string CarName => _carName;

        public int Position => position;

        public string CarNumber => _carNumber;
        
        public bool InPitLane => _inPitLane;

        private string BuildMetricName(string metricSuffix)
        {
            return string.Format(Prefix, position, metricSuffix);
        }

        private double ReadDouble(string metricName)
        {
            var actual = pluginAdapter.GetPropertyValue(
            BuildMetricName(metricName));

            if (actual == null || actual == "No Data") // To be tested
            {
                return 0;
            }
            return Convert.ToDouble(actual);
        }

        private string ReadString(string metricName)
        {
            var actual = pluginAdapter.GetPropertyValue(
            BuildMetricName(metricName));

            if (actual == null)
            {
                return "N/A";
            }
            return actual.ToString();
        }

        private int ReadInteger(string metricName) // To be tested
        {
            var actual = pluginAdapter.GetPropertyValue(
           BuildMetricName(metricName));

            if (actual == null || actual == "No Data") // To be tested
            {
                return 0;
            }
            return Convert.ToInt32(actual);
        }

    }
}
