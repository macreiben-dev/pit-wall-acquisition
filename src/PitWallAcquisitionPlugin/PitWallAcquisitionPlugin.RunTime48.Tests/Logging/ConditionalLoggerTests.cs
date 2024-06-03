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
            _logger = a =>
            {
                _actualMessage = a;
                _invokeCount++;
            };

            _invokeCount = 0;
            _actualMessage = null;
        }

        private static ConditionalLogger GetTarget()
        {
            return new ConditionalLogger();
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

        [Fact]
        public void GIVEN_message_is_different_THEN_invoke_twice()
        {
            // ARRANGE

            // ACT
            var target = GetTarget();

            target.Info(OriginalMessage, _logger);
            target.Info(OriginalMessage + "02", _logger);

            // ASSERT
            Check.That(_invokeCount).IsEqualTo(2);
        }
        
        [Fact]
        public void GIVEN_message_is_different_THEN_invoke_twice_with_differente_message()
        {
            // ARRANGE
            string actualMessage01 = null;
            string actualMessage02 = null;

            var originalMessage01 = OriginalMessage;
            var originalMessage02 = OriginalMessage + "02";
            
            // ACT
            var target = GetTarget();

            target.Info(originalMessage01, _logger);
            actualMessage01 = _actualMessage;
            
            target.Info(originalMessage02, _logger);
            actualMessage02 = _actualMessage;

            // ASSERT
            Check.That(actualMessage01).IsEqualTo(originalMessage01);
            Check.That(actualMessage02).IsEqualTo(originalMessage02);
        }

    }
}