using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFluent;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using PitWallAcquisitionPlugin.IntegrationTests;
using System.Diagnostics;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Aggregators
{
    
    public class LiveAggregatorTest
    {
        private FakePitWallConfiguration _configuration;

        public LiveAggregatorTest()
        {
            _configuration = new FakePitWallConfiguration();
        }

        private TelemetryLiveAggregator GetTarget()
        {
            return new TelemetryLiveAggregator(_configuration, new MappingConfigurationRepository());
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

            target.SetSessionTimeLeft(original);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

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

            _configuration.PilotName = original;

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            // ASSERT
            Check.That(actual.PilotName).IsEqualTo("PilotName01");

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }


        [Fact]
        public void Should_add_carName()
        {
            // ARRANGE
            var original = "CarName01";

            var target = GetTarget();

            _configuration.CarName = original;

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            // ASSERT
            Check.That(actual.CarName).IsEqualTo("CarName01");

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
        
        public class LapTimes
        {
            private FakePitWallConfiguration _configuration;

            public LapTimes()
            {
                _configuration = new FakePitWallConfiguration();
            }
            private TelemetryLiveAggregator GetTarget()
            {
                return new TelemetryLiveAggregator(_configuration, new MappingConfigurationRepository());
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

                target.SetLaptime(original);

                watch.Stop();

                var actual = (ITelemetryData)target.AsData();

                // ASSERT
                Check.That(actual.LaptimeSeconds).IsEqualTo(122.0);
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

                target.SetLaptime(original);

                watch.Stop();

                var actual = (ITelemetryData)target.AsData();

                // ASSERT
                Check.That(actual.LaptimeSeconds).IsNull();
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

                target.SetLaptime(original);

                watch.Stop();

                var actual = (ITelemetryData)target.AsData();

                // ASSERT
                Check.That(actual.LaptimeSeconds).IsNull();
                Check.That(target.IsDirty).IsFalse();
            }

        }

    }
}
