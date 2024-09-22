namespace PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators.Models
{
    internal sealed class TyresWear : ITyresWear
    {
        public double? FrontLeftWear { get; set; }

        public double? FrontRightWear { get; set; }

        public double? RearLeftWear { get; set; }

        public double? RearRightWear { get; set; }
    }
}
