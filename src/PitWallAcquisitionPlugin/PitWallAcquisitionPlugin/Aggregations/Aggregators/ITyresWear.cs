namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface ITyresWear
    {
        double? FrontLeftWear { get; }
        double? FrontRightWear { get; }
        double? RearRightWear { get; }
        double? ReartLeftWear { get; }
    }
}