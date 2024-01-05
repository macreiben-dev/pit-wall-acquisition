namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public interface ISendableData
    {
        string SimerKey { get; }

        string PilotName { get; }

        string CarName { get; }

        object AsData();
    }
}