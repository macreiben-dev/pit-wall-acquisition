using System;

namespace PitWallAcquisitionPlugin.Logging
{
    public sealed class ConditionalLogger : IConditionalLogger
    {
        private const string PreviousMessageWasTheSameAsTheCurrentOneSkipping = "Previous message was the same as the current one. Skipping.";
        private string _previousMessage;
        private bool _notifiedSameMessage;

        public void Log(string originalMessage,
            Action<string> logger)
        {
            if (originalMessage == _previousMessage)
            {
                if (!_notifiedSameMessage)
                {
                    logger(PreviousMessageWasTheSameAsTheCurrentOneSkipping);

                    _notifiedSameMessage = true;
                }

                return;
            }

            logger(originalMessage);

            _previousMessage = originalMessage;
        }
    }
}