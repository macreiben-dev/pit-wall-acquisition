namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public interface ISettingsValidator
    {
        string IsApiAddressValid(string value);
        string IsCarNameValid(string value);
        string IsPersonalKeyValid(string value);
        string IsPilotNameValid(string value);
    }
}