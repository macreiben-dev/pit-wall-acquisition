using NFluent;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories;

namespace PitWallAcquisitionPlugin.IntegrationTests.Repositories
{
    public class PitWallRemoteRepositoryTest
    {
        [Fact]
        public void Should_contact_api()
        {
            FakePitWallConfiguration configuration = new FakePitWallConfiguration()
            {
                ApiAddress = "http://localhost:32773",
                PersonalKey = "some_test_looking_value23",
                PilotName = "Pilot_IntegrationTestFromPlugin",
                CarName = "Car_IntegrationTestsFromPlugin"
            };

            PitWallTelemetryRemoteRepositoryLegacy target = new PitWallTelemetryRemoteRepositoryLegacy(configuration);

            ITelemetryLiveAggregator aggregater = new TelemetryLiveAggregator(
                configuration, 
                new MappingConfigurationRepository());

            aggregater.SetLaptime("00:02:02.0000000");

            aggregater.SetFrontLeftTyreWear(50.0);
            aggregater.SetFrontRightTyreWear(51.0);
            aggregater.SetRearLeftTyreWear(52.0);
            aggregater.SetRearRightTyreWear(53.0);

            aggregater.SetFrontLeftTyreTemperature(45.0);
            aggregater.SetFrontRightTyreTemperature(46.0);
            aggregater.SetRearRightTyreTemperature(47.0);
            aggregater.SetRearRightTyreTemperature(48.0);

            aggregater.SetAvgWetness(0.15);
            aggregater.SetAirTemperature(15.8);
            aggregater.SetSessionTimeLeft("00:03:03.0000000");

            var data = aggregater.AsData();

            Check.ThatCode(() => target.SendAsync(data)).DoesNotThrow();
        }
    }
}
