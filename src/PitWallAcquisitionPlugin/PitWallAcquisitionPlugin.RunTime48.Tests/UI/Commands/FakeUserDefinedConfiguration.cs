using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.UI.Commands
{
    public class FakeUserDefinedConfiguration : IUserDefinedConfiguration
    {
        public FakeUserDefinedConfiguration()
        {
        }

        public string PilotName
        {
            get;
            set;
        }

        public string ApiAddress
        {
            get;
            set;
        }

        public string PersonalKey
        {
            get;
            set;
        }

        public string CarName
        {
            get;
            set;
        }
    }
}
