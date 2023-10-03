using NFluent;
using PitWallAcquisitionPlugin.Aggregations;
using System.Diagnostics;

namespace PitWallAcquisitionPlugin.Tests.Aggregations
{
    public class LiveAggregatorTest
    {
        private LiveAggregator GetTarget()
        {
            return new LiveAggregator();
        }

        [Fact]
        public void Should_notBe_dirty_by_default()
        {
            var aggregator = GetTarget();

            Check.That(aggregator.IsDirty).IsFalse();
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

        // ===== Laptime milliseconds

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
            Check.That(actual.LaptimeMilliseconds).IsEqualTo(122000);
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Should_notAdd_lapTimeMilliseconds_WHEN_null()
        {
            // ARRANGE
            string original = null;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddLaptime(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.LaptimeMilliseconds).IsNull();
            Check.That(target.IsDirty).IsFalse();
        }

        [Fact]
        public void Should_notAdd_lapTimeMilliseconds_WHEN_empty()
        {
            // ARRANGE
            string original = "";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddLaptime(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.LaptimeMilliseconds).IsNull();
            Check.That(target.IsDirty).IsFalse();
        }

        // ===== Laptime milliseconds ___EOF___

        // ===== Tyre wear front left

        [Fact]
        public void GIVEN_frontLeftTyreWear_isNotNull_THEN_data_frontLeftTyreWear_isNotNull()
        {
            // ARRANGE
            double original = 85.000000001;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontLeftTyreWear(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.FrontLeftWear).IsEqualTo(85.000000001);
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_frontLeftTyreWear_isNull_WHEN_tyreWear_isNull()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontLeftTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.FrontLeftWear).IsNull();
            Check.That(target.IsDirty).IsFalse();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_frontLeftTyreValue_isNull_THEN_isDirty_isFalse()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontLeftTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        // ===== Tyre wear front left  ___EOF___

        // ===== Tyre wear front right

        [Fact]
        public void GIVEN_frontRightTyreWear_isNotNull_THEN_data_frontRightTyreWear_isNotNull()
        {
            // ARRANGE
            double original = 85.000000001;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontRightTyreWear(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.FrontRightWear).IsEqualTo(85.000000001);
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_frontRightTyreWear_isNull_THEN_data_frontRightTyreWear_isNull()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontRightTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.FrontRightWear).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_frontRightTyreWearValue_is_null_THEN_isDirty_isFalse()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontRightTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        // ===== Tyre wear front right ___EOF___

        // ===== Tyre wear rear left

        [Fact]
        public void GIVEN_rearLeftTyreWear_isNotNull_THEN_data_rearLeftTyreWear_isNotNull()
        {
            // ARRANGE
            double original = 85.000000001;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddRearLeftTyreWear(original);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.ReartLeftWear).IsEqualTo(85.000000001);
            Check.That(target.IsDirty).IsTrue();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_rearLeftTyreWear_isNull_WHEN_tyreWear_isNull()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddRearLeftTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.Tyres.ReartLeftWear).IsNull();
            Check.That(target.IsDirty).IsFalse();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void GIVEN_rearLeftTyreValue_isNull_THEN_isDirty_isFalse()
        {
            // ARRANGE
            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddRearLeftTyreWear(null);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        // ===== Tyre wear rear left  ___EOF___

        // ===== Clear

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_laptime_is_null()
        {
            // ARRANGE
            string original = "00:02:02.000";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddLaptime(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.LaptimeMilliseconds).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_sessionTimeLeft_is_null()
        {
            // ARRANGE
            string original = "00:02:02.000";

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddLaptime(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.SessionTimeLeft).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
    }
}
