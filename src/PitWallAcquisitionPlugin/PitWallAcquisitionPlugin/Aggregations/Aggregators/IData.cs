namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface IData
    {
        string SimerKey { get; }

        double? LaptimeSeconds { get; }

        string PilotName { get; }

        string SessionTimeLeft { get; }

        double? AvgWetness { get; }

        double? AirTemperature { get; }

        ITyresWear TyresWear { get; }

        ITyresTemperatures TyresTemperatures { get; }
    }
}