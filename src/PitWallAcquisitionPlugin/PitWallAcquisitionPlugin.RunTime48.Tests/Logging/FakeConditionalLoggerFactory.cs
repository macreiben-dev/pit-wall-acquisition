using PitWallAcquisitionPlugin.Logging;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class FakeConditionalLoggerFactory : IConditionalLoggerFactory
    {
        public IConditionalLogger CreateLogger()
        {
            return new FakeConditionalLogger();
        }
    }
}