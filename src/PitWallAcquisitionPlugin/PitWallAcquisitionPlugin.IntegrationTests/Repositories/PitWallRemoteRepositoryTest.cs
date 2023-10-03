using NFluent;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.IntegrationTests.Repositories
{
    public class PitWallRemoteRepositoryTest
    {
        [Fact]
        public void Should_contact_api()
        {
            PitWallRemoteRepository target = new PitWallRemoteRepository();

            ILiveAggregator aggregater = new LiveAggregator();

            aggregater.AddLaptime("00:02:02.0000000");

            aggregater.AddFrontLeftTyreWear(50.0);
            aggregater.AddFrontRightTyreWear(51.0);
            aggregater.AddRearLeftTyreWear(52.0);
            aggregater.AddRearRightTyreWear(53.0);

            var data = aggregater.AsData();

            Check.ThatCode(() => target.SendAsync(data)).DoesNotThrow();
        }
    }
}
