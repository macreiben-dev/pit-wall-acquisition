namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
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

    public class VehicleConsumption : IVehicleConsumption
    {
        public double? Fuel { get; set; }

        public double? ComputedLastLapConsumption { get; set; }

        public double? MaxFuel { get; set; }

        public double? ComputedLiterPerLaps { get; set; }

        public double? ComputedRemainingLaps { get; set; }

        public double? ComputedRemainingTime { get; set; }
    }
}