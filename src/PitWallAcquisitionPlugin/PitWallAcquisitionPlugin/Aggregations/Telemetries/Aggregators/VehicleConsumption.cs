namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators
{
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