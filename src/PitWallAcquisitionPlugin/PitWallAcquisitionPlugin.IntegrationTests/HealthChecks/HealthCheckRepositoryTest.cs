using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.HealthChecks.Repositories;

namespace PitWallAcquisitionPlugin.IntegrationTests.HealthChecks
{
    public class HealthCheckRepositoryTest
    {
        private HealthCheckRepository GetTarget()
        {
            return new HealthCheckRepository(Substitute.For<ILogger>());
        }

        [Fact]
        public async void GIVEN_apiAddress_isValid_THEN_returnTrue()
        {
            var target = GetTarget();

            var actual =  await target.Check("http://localhost:32773");

            Check.That(actual).IsTrue();
        }

        [Fact]
        public async void GIVEN_apiAddress_isNotReachable_THEN_returnFalse()
        {
            var target = GetTarget();

            var actual = await target.Check("http://localhostttt:32773");

            Check.That(actual).IsFalse();
        }
    }
}
