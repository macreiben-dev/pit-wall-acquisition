namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public interface IPitWallConfiguration
    {
        string ApiAddress { get; set; }
        string PersonalKey { get; set; }
        string PilotName { get; set; }
    }
}