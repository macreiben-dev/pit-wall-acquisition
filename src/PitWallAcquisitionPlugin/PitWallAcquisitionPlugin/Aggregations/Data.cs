﻿using SimHub.Plugins.OutputPlugins.GraphicalDash.Models.BuiltIn.Enums;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public sealed class Data : IData
    {
        public Data()
        {
            Tyres = new Tyres();
        }

        public string PilotName { get; set; }

        public double LaptimeMilliseconds { get; set; }

        public ITyres Tyres { get; set; }

        public string SessionTimeLeft { get; set; } = string.Empty;
    }
}
