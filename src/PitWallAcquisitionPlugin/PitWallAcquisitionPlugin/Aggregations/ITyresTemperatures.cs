namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public interface ITyresTemperatures
    {
        double? FrontLeftTemp { get; }
        double? FrontRightTemp { get;  }
        double? RearLeftTemp { get; }
        double? RearRightTemp { get; }
    }
}