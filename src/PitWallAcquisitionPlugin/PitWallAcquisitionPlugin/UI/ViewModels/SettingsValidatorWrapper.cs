using System;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public sealed class SettingsValidatorWrapper : ISettingsValidator
    {
        public string GetPilotNameIssueMessage(string value)
        {
            return DataValidator.PilotName.IsValid(value);
        }

        public string GetCarNameIssueMessage(string value)
        {
            return DataValidator.CarName.IsValid(value);
        }

        public string GetApiAddressIssueMessage(string value)
        {
            return DataValidator.ApiAddress.IsValid(value);
        }

        public string GetPersonalKeyIssueMessage(string value)
        {
            return DataValidator.PersonalKey.IsValid(value);
        }

        // ----

        public bool IsPersonalKeyValid(string value)
        {
            return GetPersonalKeyIssueMessage(value) == null;
        }

        public bool IsApiAddressValid(string value)
        {
            return GetApiAddressIssueMessage(value) == null;
        }

        public bool IsCarValid(string value)
        {
            return GetCarNameIssueMessage(value) == null;
        }

        public bool IsPilotValid(string input)
        {
            return GetPilotNameIssueMessage(input) == null;
        }
    }

}
