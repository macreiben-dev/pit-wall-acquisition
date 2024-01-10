using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using System;

namespace DemoProject.RunTime48.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GIVEN_something_THEN_fail()
        {
            Check.That(1).IsEqualTo(2);
        }
    }
}
