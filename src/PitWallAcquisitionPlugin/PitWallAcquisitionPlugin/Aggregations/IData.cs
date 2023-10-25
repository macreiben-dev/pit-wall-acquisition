namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public interface IData
    {
        string SimerKey { get; }

        double? LaptimeSeconds { get; }

        string PilotName { get; }

        string SessionTimeLeft { get; }

        double? AvgWetness { get; }

        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }
    }
}