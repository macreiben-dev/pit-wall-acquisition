using NFluent;

namespace PitWallAcquisitionPlugin.Tests
{
    public class WebApiForwarderTest
    {
        [Fact]
        public void Should_build_webapi_forwarder()
        {
            Check.ThatCode(() => { new WebApiForwarder(); });
        }
    }
}
