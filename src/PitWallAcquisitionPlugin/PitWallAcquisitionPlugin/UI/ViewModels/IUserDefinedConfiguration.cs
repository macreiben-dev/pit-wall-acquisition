namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    internal interface IUserDefinedConfiguration
    {
        string PilotName
        {
            get;
        }

        string ApiAddress
        {
            get;
        }

        string PersonalKey
        {
            get;
        }
    }
}
