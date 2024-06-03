using System;
using NFluent;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class ConditionalLoggerTests
    {
        private readonly Action<string> _logger;
        private string _actualMessage;
        private int _invokeCount;
        private const string OriginalMessage = "Some original message";

        public ConditionalLoggerTests()
        {
            _logger = new Action<string>(a =>
            {
                _actualMessage = a;
                _invokeCount++;
            });

            _invokeCount = 0;
            _actualMessage = null;
        }

        [Fact]
        public void GIVEN_message_is_same_THEN_do_not_invoke()
        {
            // ACT
            var target = new ConditionalLogger();

            target.Info(OriginalMessage, _logger);
            target.Info(OriginalMessage, _logger);

            // ASSERT
            Check.That(_invokeCount).IsEqualTo(1);
        }

        [Fact]
        public void GIVEN_invoke_once_THEN_invoke()
        {
            // ARRANGE

            // ACT
            var target = new ConditionalLogger();

            target.Info(OriginalMessage, _logger);

            // ASSERT
            Check.That(_invokeCount).IsEqualTo(1);
            Check.That(_actualMessage).IsEqualTo(OriginalMessage);
        }
    }
}