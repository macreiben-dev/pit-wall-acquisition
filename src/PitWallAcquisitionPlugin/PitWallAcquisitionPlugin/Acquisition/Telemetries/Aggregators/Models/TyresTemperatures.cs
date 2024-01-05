namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models
{
    public sealed class TyresTemperatures : ITyresTemperatures
    {
        public TyresTemperatures()
        {
        }

        public double? FrontLeftTemp { get; set; }
        public double? FrontRightTemp { get; set; }
        public double? RearLeftTemp { get; set; }
        public double? RearRightTemp { get; set; }
    }
}