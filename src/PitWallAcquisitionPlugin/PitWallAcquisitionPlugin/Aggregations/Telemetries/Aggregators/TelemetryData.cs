using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators
{
    public sealed class TelemetryData : ITelemetryData
    {
        private readonly Version _version;

        public TelemetryData()
        {
            _version = new Version(1, 0);
            TyresWear = new TyresWear();

            TyresTemperatures = new TyresTemperatures();
        }

        public string PilotName { get; set; }
        public string CarName { get; set; }

        public double? LaptimeSeconds { get; set; }

        public ITyresWear TyresWear { get; set; }

        public ITyresTemperatures TyresTemperatures { get; set; }

        public string SessionTimeLeft { get; set; } = string.Empty;

        public string SimerKey { get; set; }

        public double? AvgWetness { get; set; }

        public double? AirTemperature { get; set; }
        public double? TrackTemperature { get; set; }
        public double? Fuel { get; set; }
        public IVehicleConsumption VehicleConsumption { get; set; }
    }
}
