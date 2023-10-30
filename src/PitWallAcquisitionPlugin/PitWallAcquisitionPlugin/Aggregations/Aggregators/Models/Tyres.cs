namespace PitWallAcquisitionPlugin.Aggregations.Aggregators.Models
{
    public sealed class TyresWear : ITyresWear
    {
        public double? FrontLeftWear { get; set; }

        public double? FrontRightWear { get; set; }

        public double? ReartLeftWear { get; set; }

        public double? RearRightWear { get; set; }
    }
}
