using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.Threading.Tasks;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Acquisitions.Repositories
{
    public sealed class RemoteRepositoriesTest
    {
        private readonly IPitwallRemoteClient _client;
        private readonly IPitWallConfiguration _configuration;

        public RemoteRepositoriesTest()
        {
            _client = Substitute.For<IPitwallRemoteClient>();

            _configuration = Substitute.For<IPitWallConfiguration>();
        }

        [Fact]
        public async Task GIVEN_a_remote_type_telemetry_THEN_return_instance()
        {
            RemotesRepository remotesRepository = GetTarget();

            var actual = remotesRepository.SelectFrom(RemoteTypeEnum.Telemetry);

            // ASSERT
            Check.That(actual.Uri).IsEqualTo("/api/v1/telemetry");
        }

        [Fact]
        public async Task GIVEN_a_remote_type_leaderboard_THEN_return_instance()
        {
            RemotesRepository remotesRepository = GetTarget();

            var actual = remotesRepository.SelectFrom(RemoteTypeEnum.Leaderboard);

            // ASSERT
            Check.That(actual.Uri).IsEqualTo("/api/v1/leaderboard");
        }


        [Fact]
        public async Task GIVEN_a_remote_type_none_THEN_fail()
        {
            RemotesRepository remotesRepository = GetTarget();

            Check.ThatCode(() => remotesRepository.SelectFrom(RemoteTypeEnum.None))
                .Throws<ArgumentException>();

        }

        private RemotesRepository GetTarget()
        {
            // ACT
            return new RemotesRepository(_configuration);
        }
    }
}
