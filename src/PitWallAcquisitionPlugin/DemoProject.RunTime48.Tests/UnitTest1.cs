using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin;
using Xunit;

namespace DemoProject.RunTime48.Tests
{

    public class UnitTest1
    {
        [Fact]
        public void GIVEN_something_THEN_fail()
        {
            var temp = Substitute.For<IAcquisitionService>();

            Check.That(1).IsEqualTo(2);
        }
    }
}
