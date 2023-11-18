using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    internal class FakePitWallConfiguration : IPitWallConfiguration
    {
        public string ApiAddress
        {
            get; set;
        }
        public string PersonalKey
        {
            get; set;
        }
        public string PilotName
        {
            get; set;
        }
        public string CarName
        {
            get; set;
        }
    }
}