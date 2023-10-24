using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.IntegrationTests
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
    }
}