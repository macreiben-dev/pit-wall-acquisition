using NFluent;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests
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
