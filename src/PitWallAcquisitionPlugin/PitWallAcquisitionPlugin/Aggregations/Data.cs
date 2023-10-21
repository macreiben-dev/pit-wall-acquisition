﻿using System;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public sealed class Data : IData
    {
        private readonly Version _version;

        public Data()
        {
            _version = new Version(1, 0);
            TyresWear = new Tyres();

            TyresTemperatures = new TyresTemperatures();
        }

        public string PilotName { get; set; }

        public double? LaptimeSeconds { get; set; }

        public ITyresWear TyresWear { get; set; }
        
        public ITyresTemperatures TyresTemperatures { get; set; }

        public string SessionTimeLeft { get; set; } = string.Empty;
        
        public string SimerKey { get; set; }
    }
}
