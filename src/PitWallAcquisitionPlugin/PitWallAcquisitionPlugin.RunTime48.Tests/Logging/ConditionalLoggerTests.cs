using System;
using NFluent;
using PitWallAcquisitionPlugin.Logging;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class ConditionalLoggerTests
    {
        private readonly Action<string> _logger;
        private string _actualMessage;
        private string _repeatedMessage;
        private int _invokeCount;
        private const string OriginalMessage = "Some original message";
        private const string PreviousMessageWasTheSameAsTheCurrentOneSkipping = "Previous message was the same as the current one. Skipping.";

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
        public void GIVEN_invoke_once_THEN_invoke()
        {
            // ARRANGE

            // ACT
            var target = new ConditionalLogger();

            target.Log(OriginalMessage, _logger);

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

            target.Log(OriginalMessage, _logger);
            target.Log(OriginalMessage + "02", _logger);

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

            target.Log(originalMessage01, _logger);
            actualMessage01 = _actualMessage;
            
            target.Log(originalMessage02, _logger);
            actualMessage02 = _actualMessage;

            // ASSERT
            Check.That(actualMessage01).IsEqualTo(originalMessage01);
            Check.That(actualMessage02).IsEqualTo(originalMessage02);
        }

        [Fact]
        public void GIVEN_message_is_repeated_twice_THEN_log_repeatedMessage_warn()
        {
            // ACT
            var target = GetTarget();
            
            target.Log(OriginalMessage, _logger);
            target.Log(OriginalMessage, _logger);

            // ASSERT
            Check.That(_actualMessage).IsEqualTo(PreviousMessageWasTheSameAsTheCurrentOneSkipping);
        }

        [Fact]
        public void GIVEN_message_repeated_threetimes_THEN_log_message_one_AND_repeatedMessace_once()
        {
            // ACT
            var target = GetTarget();
            
            target.Log(OriginalMessage, _logger);
            
            Check.That(_actualMessage).IsEqualTo(OriginalMessage);
            
            target.Log(OriginalMessage, _logger);
            
            Check.That(_actualMessage).IsEqualTo(PreviousMessageWasTheSameAsTheCurrentOneSkipping);

            target.Log(OriginalMessage, _logger);

            Check.That(_invokeCount).IsEqualTo(2);
        }

        [Fact]
        public void GIVEN_three_different_messages_THEN_log_messages()
        {
            // ARRANGE
            string actualMessage01 = null;
            string actualMessage02 = null;
            string actualMessage03 = null;

            var originalMessage01 = OriginalMessage;
            var originalMessage02 = OriginalMessage + "02";
            var originalMessage03 = OriginalMessage + "03";
            
            // ACT
            var target = GetTarget();

            target.Log(originalMessage01, _logger);
            actualMessage01 = _actualMessage;
            
            target.Log(originalMessage02, _logger);
            actualMessage02 = _actualMessage;
            
            target.Log(originalMessage03, _logger);
            actualMessage03 = _actualMessage;

            // ASSERT
            Check.That(actualMessage01).IsEqualTo(originalMessage01);
            Check.That(actualMessage02).IsEqualTo(originalMessage02);
            Check.That(actualMessage03).IsEqualTo(originalMessage03);

        }
    }
}