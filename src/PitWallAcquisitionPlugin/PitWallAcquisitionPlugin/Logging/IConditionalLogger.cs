using System;

namespace PitWallAcquisitionPlugin.Logging
{
    public interface IConditionalLogger
    {
        void Log(string originalMessage,
            Action<string> logger);
    }
}