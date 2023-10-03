namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public interface IData
    {
        double? LaptimeMilliseconds { get; }
        string PilotName { get; }
        string SessionTimeLeft { get; }
        ITyres Tyres { get; }
    }
}