namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public sealed class Tyres : ITyresWear
    {
        public double? FrontLeftWear { get; set; }

        public double? FrontRightWear { get; set; }

        public double? ReartLeftWear { get; set; }

        public double? RearRightWear { get; set; }
    }
}
