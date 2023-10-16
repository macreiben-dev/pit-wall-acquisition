using FluentAssertions;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Repositories;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    public sealed class IsApiAvailableCommandTest
    {
        private const string VALID_API_ADDRESS = "http://locallhost:32773";
        private const string INVALID_API_ADDRESS = "httttttp://locallhost:32773";
        private IPitWallApiStatusRepository _apiAvailabilityRepo;
        private IDisplayAvailability _viewModel;

        public IsApiAvailableCommandTest()
        {
            _apiAvailabilityRepo = Substitute.For<IPitWallApiStatusRepository>();

            _viewModel = new FakeDisplayAvailability();
        }

        private IsApiAvailableCommand GetTarget()
        {
            return new IsApiAvailableCommand(
                _viewModel, 
                _apiAvailabilityRepo);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void GIVEN_apiAddress_isNotSet_THEN_canExecute_returns_false(string input)
        {
            IsApiAvailableCommand target = GetTarget();

            var actual = target.CanExecute(input);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public void GIVEN_apiAddress_isSet_AND_valid_THEN_canExecute_return_true()
        {
            var target = GetTarget();

            var actual = target.CanExecute(VALID_API_ADDRESS);

            Check.That(actual).IsTrue();
        }

        [Fact]
        public void GIVEN_apiAddress_isSet_AND_valid_THEN_execute_checkAvailability_AND_set_isAvailable_toTrue()
        {
            _apiAvailabilityRepo.IsAvailable(VALID_API_ADDRESS)
                .Returns(true);

            var target = GetTarget();

            target.Execute(VALID_API_ADDRESS);

            Check.That(_viewModel.IsApiAvailable).IsTrue();
        }

        [Fact]
        public void GIVEN_apiAddress_isSet_AND_invalid_THEN_execute_checkAvailability_AND_set_isAvailable_toFalse()
        {
            _apiAvailabilityRepo.IsAvailable(VALID_API_ADDRESS)
                .Returns(true);

            var target = GetTarget();

            target.Execute(INVALID_API_ADDRESS);

            Check.That(_viewModel.IsApiAvailable).IsFalse();
        }

        [Fact]
        public void GIVEN_raiseCanExecuteChanged_invoked_THEN_raiseEvent()
        {
            var target = GetTarget();

            using var monitored = target.Monitor();

            target.RaiseCanExecuteChanged();

            monitored.Should().Raise("CanExecuteChanged")
               .WithSender(target);
        }
    }
}
