using PitWallAcquisitionPlugin.Aggregations.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models
{
    internal interface ITelemetryData : ISendableData
    {
        double? LaptimeSeconds { get; }

        string SessionTimeLeft { get; }

        double? AvgWetness { get; }

        double? AirTemperature { get; }

        double? TrackTemperature { get; }

        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }

        IVehicleConsumption VehicleConsumption { get; }
    }
}