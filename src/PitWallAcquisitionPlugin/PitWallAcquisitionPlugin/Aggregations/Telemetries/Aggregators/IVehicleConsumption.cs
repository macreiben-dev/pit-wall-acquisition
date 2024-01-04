namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators
{
    public interface IVehicleConsumption
    {
        double? Fuel { get; }
        double? ComputedLastLapConsumption { get; }

        double? MaxFuel { get; }
        double? ComputedLiterPerLaps { get; }
        double? ComputedRemainingLaps { get; }
        double? ComputedRemainingTime { get; }
    }
}