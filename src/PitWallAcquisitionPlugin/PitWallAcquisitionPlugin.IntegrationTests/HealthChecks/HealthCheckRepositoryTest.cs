using NFluent;
using PitWallAcquisitionPlugin.HealthChecks.Repositories;

namespace PitWallAcquisitionPlugin.IntegrationTests.HealthChecks
{
    public class HealthCheckRepositoryTest
    {
        private HealthCheckRepository GetTarget()
        {
            return new HealthCheckRepository();
        }

        [Fact]
        public async void GIVEN_apiAddress_isValid_THEN_returnTrue()
        {
            var target = GetTarget();

            var actual =  await target.Check("http://localhost:32773");

            Check.That(actual).IsTrue();
        }
    }
}
