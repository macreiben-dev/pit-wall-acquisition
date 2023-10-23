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
        public void GIVEN_apiAddress_isValid_THEN_returnTrue()
        {

        }
    }
}
