namespace PitWallAcquisitionPlugin.Logging
{
    public interface IConditionalLoggerFactory
    {
        IConditionalLogger CreateLogger();
    }
}