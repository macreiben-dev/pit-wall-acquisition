using NFluent;
using PitWallAcquisitionPlugin.Aggregations;
using System.Diagnostics;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests.Aggregations
{
    public class LiveAggregatorTest
    {
        private LiveAggregator GetTarget()
        {
            return new LiveAggregator();
        }

        [Fact]
        public void Should_trim_sessionLeft()
        {
            // ARRANGE
            var original = "00:56:39.1970000";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddSessionTimeLeft(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.SessionTimeLeft).IsEqualTo("00:56:39");
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Should_add_pilotName()
        {
            // ARRANGE
            var original = "PilotName01";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddPilotName(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.PilotName).IsEqualTo("PilotName01");
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Should_add_lapTimeMilliseconds()
        {
            // ARRANGE
            string original = "00:02:02.000";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddLaptime(original);

            watch.Stop();

            var actual = target.AsData();
             
            // ASSERT
            Check.That(actual.LaptimeMilliseconds).IsEqualTo(120.0);
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
    }
}
