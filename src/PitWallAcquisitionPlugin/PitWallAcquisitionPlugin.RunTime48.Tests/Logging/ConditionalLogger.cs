using System;
using System.Security.Permissions;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class ConditionalLogger
    {
        private string _previousMessage;

        public void Info(string originalMessage,
            Action<string> logger)
        {
            if (originalMessage == _previousMessage)
            {
                return;
            }

            logger(originalMessage);
            _previousMessage = originalMessage;
        }
    }
}