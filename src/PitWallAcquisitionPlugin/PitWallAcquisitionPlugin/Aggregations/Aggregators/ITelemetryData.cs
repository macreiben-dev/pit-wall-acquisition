using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;

namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface ITelemetryData : ISendableData
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