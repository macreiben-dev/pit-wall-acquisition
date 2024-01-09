namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    internal interface ISendableData
    {
        string SimerKey { get; }

        string PilotName { get; }

        string CarName { get; }

        object AsData();
    }
}