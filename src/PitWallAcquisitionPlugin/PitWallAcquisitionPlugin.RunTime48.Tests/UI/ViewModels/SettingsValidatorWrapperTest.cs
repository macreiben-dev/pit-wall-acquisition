using PitWallAcquisitionPlugin.UI.ViewModels;
using NFluent;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class SettingsValidatorWrapperTest
    {
        private const string VALID_API_ADDRESS = "http://api.address.net";

        private SettingsValidatorWrapper GetTarget()
        {
            return new SettingsValidatorWrapper();
        }
        #region ApiAddress

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("12345678")]
        [InlineData("123456789")]
        public void GIVEN_personalKey_isLessThan_10character_THEN_returnMessage(string input)
        {
            var target = GetTarget();

            var actual = target.GetPersonalKeyIssueMessage(input);

            Check.That(actual).IsEqualTo("Personal key length should be at least 10 character long.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void GIVEN_personalKey_isNullOrEmptyOrWhiteSpace_THEN_returnMessage(string input)
        {
            var target = GetTarget();

            var actual = target.GetPersonalKeyIssueMessage(input);

            Check.That(actual)
                .IsEqualTo("Personal should be made of alphanumerical character and \"-\", \"_\", \"@\".");
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        [InlineData("1234567")]
        [InlineData("12345678")]
        [InlineData("123456789")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("     ")]
        public void GIVEN_personalKey_isInvalid_THEN_returnFalse(string input)
        {
            var target = GetTarget();

            var actual = target.IsPersonalKeyValid(input);

            Check.That(actual).IsFalse();
        }

        #endregion ApiAddress

        // ----------------------

        #region ApiAddress

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_ApiAddress_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            var target = GetTarget();

            var actual = target.GetApiAddressIssueMessage(input);

            Check.That(actual).IsEqualTo("API address must be set.");
        }

        [Fact]
        public void GIVEN_apiAddress_uri_isValid_THEN_error_isNull()
        {
            string input = VALID_API_ADDRESS;

            var target = GetTarget();

            var actual = target.GetApiAddressIssueMessage(input);

            Check.That(actual).IsNull();
        }


        [Theory]
        [InlineData("htttttp://....ext")]
        [InlineData("http://test,test2.ext")]
        [InlineData("http://test=test2.ext")]
        public void GIVEN_apiAddress_uri_isNotValid_THEN_error_isSet(string input)
        {
            var target = GetTarget();

            var actual = target.GetApiAddressIssueMessage(input);

            Check.That(actual).IsEqualTo(
                "API URI format is invalid. Should look like http://domain.ext or http://domain.ext");
        }

        [Theory]
        [InlineData("htttttp://....ext")]
        [InlineData("http://test,test2.ext")]
        [InlineData("http://test=test2.ext")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_apiAddress_isInvalid_THEN_returnFalse(string input)
        {
            var target = GetTarget();

            var actual = target.IsApiAddressValid(input);

            Check.That(actual).IsFalse();
        }

        [Fact]
        public void GIVEN_apiAddress_uri_isValid_THEN_returnTrue()
        {
            string input = VALID_API_ADDRESS;

            var target = GetTarget();

            var actual = target.IsApiAddressValid(input);

            Check.That(actual).IsTrue();
        }
        #endregion ApiAddress

        // ----------------------

        #region CarName

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_CarName_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            var target = GetTarget();

            var actual = target.GetCarNameIssueMessage(input);

            Check.That(actual).IsEqualTo("Car name must be set.");
        }

        [Theory]
        [InlineData("CarName1")]
        [InlineData("CarName2")]
        [InlineData("Car2")]
        [InlineData("Car")]
        public void GIVEN_carName_isValid_THEN_return_null(string input)
        {
            var target = GetTarget();

            var actual = target.GetCarNameIssueMessage(input);

            Check.That(actual).IsNull();
        }

        [Theory]
        [InlineData("CarName1")]
        [InlineData("CarName2")]
        [InlineData("Car2")]
        [InlineData("Car")]
        public void GIVEN_carName_isValid_THEN_return_true(string input)
        {
            var target = GetTarget();

            var actual = target.IsCarNameValid(input);

            Check.That(actual).IsTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_carName_isInvalid_THEN_returnFalse(string input)
        {
            var target = GetTarget();

            var actual = target.IsCarNameValid(input);

            Check.That(actual).IsFalse();
        }


        #endregion CarName

        // ----------------------

        #region PilotName

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_pilotName_isNotSet_THEN_dataErrorInfo_returns_notSet(string input)
        {
            var target = GetTarget();

            var actual = target.GetPilotNameIssueMessage(input);

            Check.That(actual).IsEqualTo("Pilot name must be set.");
        }

        [Theory]
        [InlineData("Pilot1")]
        [InlineData("Pilot2")]
        [InlineData("Pilot3")]
        public void GIVEN_pilotName_isValid_THEN_return_true(string input)
        {
            var target = GetTarget();

            var actual = target.IsPilotNameValid(input);

            Check.That(actual).IsTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        [InlineData("      ")]
        public void GIVEN_pilotName_isInvalid_THEN_returnFalse(string input)
        {
            var target = GetTarget();

            var actual = target.IsPilotNameValid(input);

            Check.That(actual).IsFalse();
        }

        #endregion PilotName
    }
}
