namespace PitWallAcquisitionPlugin.Aggregations.Aggregators.Models
{
    public interface ITyresWear
    {
        double? FrontLeftWear { get; }
        double? FrontRightWear { get; }
        double? RearRightWear { get; }
        double? RearLeftWear { get; }
    }
}