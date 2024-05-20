using System;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{

    internal sealed class LeaderboardEntry : ILeaderboardEntry
    {        
        private const string Prefix = "GarySwallowDataPlugin.Leaderboard.Position{0:00}.{1}";

        private readonly IPluginManagerAdapter _pluginAdapter;
        private readonly int _position;
        private readonly double _lastLapInSeconds;
        private readonly string _carName;
        private readonly string _carNumber;
        private readonly bool _inPitLane;
        private readonly bool _inPitBox;

        public LeaderboardEntry(IPluginManagerAdapter pluginAdapter, int position)
        {
            _pluginAdapter = pluginAdapter;
            _position = position;

            _lastLapInSeconds = ReadDouble("LastLap");
            
            _carName = ReadString("CarName");
            
            _carNumber = ReadString("CarNumber");
            
            _inPitLane = ReadBool("InPitLane");

            _inPitBox = ReadBool("InPitBox");
        }

        public double LastLapInSeconds => _lastLapInSeconds;

        public string CarName => _carName;

        public int Position => _position;

        public string CarNumber => _carNumber;
        
        public bool InPitLane => _inPitLane;
        
        public bool InPitBox => _inPitBox;

        private string BuildMetricName(string metricSuffix)
        {
            return string.Format(Prefix, _position, metricSuffix);
        }

        private double ReadDouble(string metricName)
        {
            var actual = _pluginAdapter.GetPropertyValue(
            BuildMetricName(metricName));

            if (actual == null || actual == "No Data") // To be tested
            {
                return 0;
            }
            return Convert.ToDouble(actual);
        }

        private string ReadString(string metricName)
        {
            var actual = _pluginAdapter.GetPropertyValue(
            BuildMetricName(metricName));

            if (actual == null)
            {
                return "N/A";
            }
            return actual.ToString();
        }

        private int ReadInteger(string metricName) // To be tested
        {
            var actual = _pluginAdapter.GetPropertyValue(
           BuildMetricName(metricName));

            if (actual == null || actual == "No Data") // To be tested
            {
                return 0;
            }
            return Convert.ToInt32(actual);
        }

        private bool ReadBool(string metricName)
        {
            var actual = _pluginAdapter.GetPropertyValue(
                BuildMetricName(metricName));

            var intermediary = actual is int ? (int)actual : 0;
            
            if (intermediary == 1)
            {
                return true;
            }
            
            return false;
        }
    }
}
