namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    public class FakeDisplayAvailability : IDisplayAvailability
    {
        public bool IsApiAvailable
        {
            get; 
            set;
        }
    }
}
