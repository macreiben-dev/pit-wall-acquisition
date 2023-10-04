namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public interface IData
    {
        double? LaptimeSeconds { get; }
        string PilotName { get; }
        string SessionTimeLeft { get; }
        ITyres TyresWear { get; }
    }
}