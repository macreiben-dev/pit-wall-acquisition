namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public interface IUserDefinedConfiguration
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

        string CarName
        {
            get;
        }
    }
}
