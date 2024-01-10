namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models
{
    internal interface ITyresWear
    {
        double? FrontLeftWear { get; }
        double? FrontRightWear { get; }
        double? RearRightWear { get; }
        double? RearLeftWear { get; }
    }
}