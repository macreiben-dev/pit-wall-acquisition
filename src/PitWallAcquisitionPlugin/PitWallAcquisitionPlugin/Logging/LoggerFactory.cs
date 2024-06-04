using log4net;

namespace PitWallAcquisitionPlugin.Logging
{
    public class ConditionalLoggerFactory : IConditionalLoggerFactory
    {
        public IConditionalLogger CreateLogger()
        {
            return new ConditionalLogger();
        }
    }
}