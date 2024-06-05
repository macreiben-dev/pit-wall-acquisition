using System;
using log4net;
using NSubstitute;
using PitWallAcquisitionPlugin.Logging;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class SimhubLoggerTest
    {
        private readonly ILog _log;
        private readonly IConditionalLogger _conditionLogger;
        private readonly FakeConditionalLoggerFactory _conditionLoggerFactory;

        public SimhubLoggerTest()
        {
            _log = Substitute.For<ILog>();

            _conditionLogger = new FakeConditionalLogger();

            _conditionLoggerFactory = new FakeConditionalLoggerFactory();
        }

        private SimhubLogger GetTarget()
        {
            return new SimhubLogger(_log, _conditionLoggerFactory);
        }
        
        // ===========================================================
        [Fact]
        public void GIVEN_debug_invoked_once_THEN_invoke_inner_logger()
        {
            // Arrange
            var target = GetTarget();
            var message = "message";

            // Act
            target.Debug(message);

            // Assert
            _log.Received(1).Debug("PitWallAcquisitionPlugin: message");
        }

        [Fact]
        public void GIVEN_info_invoked_once_THEN_invoke_inner_logger()
        {
            // Arrange
            var target = GetTarget();
            var message = "info message";

            // Act
            target.Info(message);

            // Assert
            _log.Received(1).Info("PitWallAcquisitionPlugin: info message");
        }

        [Fact]
        public void GIVEN_warn_invoked_once_THEN_invoke_inner_logger()
        {
            // Arrange
            var target = GetTarget();
            var message = "warn message";

            // Act
            target.Warn(message);

            // Assert
            _log.Received(1).Warn("PitWallAcquisitionPlugin: warn message");
        }

        [Fact]
        public void GIVEN_error_invoked_once_THEN_invoke_inner_logger()
        {
            // Arrange
            var target = GetTarget();
            var message = "error message";

            // Act
            target.Error(message);

            // Assert
            _log.Received(1).Error("PitWallAcquisitionPlugin: error message");
        }

        [Fact]
        public void GIVEN_error_with_exception_invoked_once_THEN_invoke_inner_logger_with_exception()
        {
            // Arrange
            var target = GetTarget();
            var message = "error message";

            var ex = new Exception("Some Message from exception");

            // Act
            target.Error(message, ex);

            // Assert
            _log.Received(1).Error("PitWallAcquisitionPlugin: error message", ex);
        }
    }
}