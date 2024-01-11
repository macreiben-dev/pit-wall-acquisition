using System;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{

    internal class LeaderboardEntry
    {
        private readonly IPluginManagerAdapter pluginAdapter;
        private readonly int position;
        private readonly double _lastlapInSeconds;
        private readonly string _carName;

        public LeaderboardEntry(IPluginManagerAdapter pluginAdapter, int position)
        {
            this.pluginAdapter = pluginAdapter;
            this.position = position;

            _lastlapInSeconds = ReadDouble("LastLap");
            _carName = ReadString("CarName");
        }

        private const string Prefix = "GarySwallowDataPlugin.Leaderboard.Position{0:00}.{1}";

        public double LastLapInSeconds => _lastlapInSeconds;

        public string CarName => _carName;

        public int Position => position;

        private string BuildMetricName(string metricSuffix)
        {
            return string.Format(Prefix, position, metricSuffix);
        }

        private double ReadDouble(string metricName)
        {
            var actual = pluginAdapter.GetPropertyValue(
            BuildMetricName(metricName));

            if (actual == null)
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
    }
}
