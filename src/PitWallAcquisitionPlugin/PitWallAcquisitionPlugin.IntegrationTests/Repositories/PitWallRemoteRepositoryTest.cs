using NFluent;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Repositories;

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
                PersonalKey = "ven1_team_2023",
                PilotName = "IntegrationTestFromPlugin"
            };

            PitWallRemoteRepository target = new PitWallRemoteRepository(configuration);

            ILiveAggregator aggregater = new LiveAggregator(configuration);

            aggregater.AddLaptime("00:02:02.0000000");

            aggregater.AddFrontLeftTyreWear(50.0);
            aggregater.AddFrontRightTyreWear(51.0);
            aggregater.AddRearLeftTyreWear(52.0);
            aggregater.AddRearRightTyreWear(53.0);

            aggregater.AddFrontLeftTyreTemperature(45.0);
            aggregater.AddFrontRightTyreTemperature(46.0);
            aggregater.AddRearRightTyreTemperature(47.0);
            aggregater.AddRearRightTyreTemperature(48.0);

            aggregater.SetSimerKey("ven1_team_2023");

            var data = aggregater.AsData();

            Check.ThatCode(() => target.SendAsync(data)).DoesNotThrow();
        }
    }
}
