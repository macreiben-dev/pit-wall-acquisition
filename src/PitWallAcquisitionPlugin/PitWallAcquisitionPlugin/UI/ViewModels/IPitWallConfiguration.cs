namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal interface IPitWallConfiguration
    {
        string ApiAddress { get; set; }
        string PersonalKey { get; set; }
        string PilotName { get; set; }
        string CarName { get; set; }
    }
}