using PitWallAcquisitionPlugin.Aggregations.Aggregators.Models;

namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface IData
    {
        string SimerKey { get; }

        double? LaptimeSeconds { get; }

        string PilotName { get; }

        string CarName { get; }

        string SessionTimeLeft { get; }

        double? AvgWetness { get; }

        double? AirTemperature { get; }

        double? TrackTemperature { get; }

        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }
        

        IVehicleConsumption VehicleConsumption { get; }
    }
}