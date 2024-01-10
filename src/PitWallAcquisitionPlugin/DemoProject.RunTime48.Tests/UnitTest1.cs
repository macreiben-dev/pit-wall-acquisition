using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin;

namespace DemoProject.RunTime48.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GIVEN_something_THEN_fail()
        {
            var temp = Substitute.For<IAcquisitionService>();

            Check.That(1).IsEqualTo(2);
        }


    }
}
