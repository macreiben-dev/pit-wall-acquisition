namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public interface ISettingsValidator
    {
        string GetApiAddressIssueMessage(string value);
        string GetCarNameIssueMessage(string value);
        string GetPersonalKeyIssueMessage(string value);
        string GetPilotNameIssueMessage(string value);

        bool IsPilotNameValid(string value);
        bool IsCarNameValid(string value);
        bool IsApiAddressValid(string value);
        bool IsPersonalKeyValid(string value);
    }
}