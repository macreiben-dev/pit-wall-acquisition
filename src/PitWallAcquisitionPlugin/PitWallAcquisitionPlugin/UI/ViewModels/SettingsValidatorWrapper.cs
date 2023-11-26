namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public sealed class SettingsValidatorWrapper : ISettingsValidator
    {
        public string IsPilotNameValid(string value)
        {
            return DataValidator.PilotName.IsValid(value);
        }

        public string IsCarNameValid(string value)
        {
            return DataValidator.CarName.IsValid(value);
        }

        public string IsApiAddressValid(string value)
        {
            return DataValidator.ApiAddress.IsValid(value);
        }

        public string IsPersonalKeyValid(string value)
        {
            return DataValidator.PersonalKey.IsValid(value);
        }
    }

}
