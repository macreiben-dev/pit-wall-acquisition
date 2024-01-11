﻿namespace PitWallAcquisitionPlugin.PluginManagerWrappers.Leaderboards
{
    internal interface ILeaderboardEntry
    {
        string CarName { get; }
        double LastLapInSeconds { get; }
        int Position { get; }
    }
}