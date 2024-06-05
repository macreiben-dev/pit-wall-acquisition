using NFluent;
using PitWallAcquisitionPlugin.Logging;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Logging
{
    public class ConditionalLoggerFactoryTest
    {
        [Fact]
        public void CreateLogger_ShouldReturnIConditionalLoggerInstance()
        {
            // Arrange
            var factory = new ConditionalLoggerFactory();

            // Act
            var logger = factory.CreateLogger();

            // Assert
            Check.That(logger).IsNotNull();
            Check.That(logger).IsInstanceOf<ConditionalLogger>();
        }
    }
}