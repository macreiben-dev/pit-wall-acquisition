using System;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{

    internal class LeaderboardEntry
    {
        //private IPluginManagerAdapter _adapter;
        //private int _position;
        //private readonly string _metricName;
        //private const string Prefix = "GarySwallowDataPlugin.Leaderboard.Position{0:00}";
        //private const string BestLap = "BestLap";

        //public LeaderboardEntry(IPluginManagerAdapter adapter, int position)
        //{
        //    _adapter = adapter;
        //    _position = position;
        //}

        //public double? LastLapInScondes => TimeAsDouble(BestLap);

        //private double? TimeAsDouble(string selector)
        //{
        //    var metricName = string.Format(Prefix + "." + selector, _position);

        //    return _adapter.GetPropertyValue(metricName) as double?;
        //}
        private readonly IPluginManagerAdapter pluginAdapter;
        private readonly int position;

        public LeaderboardEntry(IPluginManagerAdapter pluginAdapter, int position)
        {
            this.pluginAdapter = pluginAdapter;
            this.position = position;
        }

        public double LasLapInSeconds => (double)pluginAdapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.LastLap");

        public string CarName => (string)pluginAdapter.GetPropertyValue("GarySwallowDataPlugin.Leaderboard.Position01.CarName");

    }
}
