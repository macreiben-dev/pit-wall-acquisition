namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface IVehicleConsumption
    {
        double? Fuel { get; }
        double? ComputedLastLapConsumption { get; }

        double? MaxFuel { get; }
    }

    public class VehicleConsumption : IVehicleConsumption
    {
        public VehicleConsumption()
        {
        }

        public double? Fuel { get; set; }

        public double? ComputedLastLapConsumption { get; set; }
        public double? MaxFuel { get; set; }
    }
}