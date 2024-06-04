using System;
using PitWallAcquisitionPlugin.Logging;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class FakeConditionalLogger : IConditionalLogger
    {
        public void Log(string originalMessage, Action<string> logger)
        {
            logger(originalMessage);
        }
    }
}