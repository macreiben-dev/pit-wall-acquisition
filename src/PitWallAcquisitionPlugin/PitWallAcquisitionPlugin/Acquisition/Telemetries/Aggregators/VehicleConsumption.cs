namespace PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators
{
    internal class VehicleConsumption : IVehicleConsumption
    {
        public double? Fuel { get; set; }

        public double? ComputedLastLapConsumption { get; set; }

        public double? MaxFuel { get; set; }

        public double? ComputedLiterPerLaps { get; set; }

        public double? ComputedRemainingLaps { get; set; }

        public double? ComputedRemainingTime { get; set; }
    }
}