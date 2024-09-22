namespace PitWallAcquisitionPlugin.Acquisition
{
    internal interface ISendableData
    {
        string SimerKey { get; }

        string PilotName { get; }

        string CarName { get; }

        object AsData();
    }
}