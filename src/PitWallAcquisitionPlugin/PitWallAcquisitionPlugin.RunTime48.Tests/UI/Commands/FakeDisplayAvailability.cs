using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    public class FakeDisplayAvailability : IDisplayAvailability
    {
        public string IsApiAvailable
        {
            get; 
            set;
        }
    }
}
