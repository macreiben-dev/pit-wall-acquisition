using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.HealthChecks.Repositories;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.HealthChecks
{
    public class HealthCheckServiceTest
    {
        private readonly IHealthCheckRepository _repo;

        public HealthCheckServiceTest()
        {
            _repo = Substitute.For<IHealthCheckRepository>();
        }

        private HealthCheckService GetTarget()
        {
            return new HealthCheckService(_repo);
        }

        [Fact]
        public async void GIVEN_apiAddressIsValid_THEN_return_true()
        {
            var originalApiAddress = "http://localhost:32773";
            _repo.Check(originalApiAddress)
                .Returns(true);

            var target = new HealthCheckService(_repo);

            bool actual = await target.Check(originalApiAddress);

            Check.That(actual).IsTrue();
        }

        [Fact]
        public async void GIVEN_apiAddressIsValid_AND_apiNotResponding_THEN_return_false()
        {
            var originalApiAddress = "http://localhost:32773";
            _repo.Check(originalApiAddress)
                .Returns(false);

            var target = new HealthCheckService(_repo);

            bool actual = await target.Check(originalApiAddress);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public async void GIVEN_apiIsGiven_AND_apiAddressIsInvalid_THEN_return_false()
        {
            var originalApiAddress = "htttttp://localhost!32773";
            _repo.Check(originalApiAddress)
                .Returns(true);
            HealthCheckService target = GetTarget();

            bool actual = await target.Check(originalApiAddress);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public async void GIVEN_apiIsNull_THEN_return_false()
        {
            HealthCheckService target = GetTarget();

            bool actual = await target.Check(null);

            Check.That(actual).IsFalse();
        }
    }
}
