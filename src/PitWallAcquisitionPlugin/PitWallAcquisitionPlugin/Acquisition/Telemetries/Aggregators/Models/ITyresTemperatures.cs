namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models
{
    public interface ITyresTemperatures
    {
        double? FrontLeftTemp { get; }
        double? FrontRightTemp { get; }
        double? RearLeftTemp { get; }
        double? RearRightTemp { get; }
    }
}