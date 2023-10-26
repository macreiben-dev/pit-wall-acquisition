﻿using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;
using NFluent;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Tests.UI.ViewModels;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Xunit;

namespace PitWallAcquisitionPlugin.Tests.Aggregations
{
    public class LiveAggregatorTest
    {
        private FakePitWallConfiguration _configuration;

        public LiveAggregatorTest()
        {
            _configuration = new FakePitWallConfiguration();
        }

        public LiveAggregator GetTarget()
        {
            return new LiveAggregator(_configuration);
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

            _configuration.PilotName = original;

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(actual.PilotName).IsEqualTo("PilotName01");

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        public class LapTimes
        {
            private FakePitWallConfiguration _configuration;

            public LapTimes()
            {
                _configuration = new FakePitWallConfiguration();
            }
            public LiveAggregator GetTarget()
            {
                return new LiveAggregator(_configuration);
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

                target.AddLaptime(original);

                watch.Stop();

                var actual = target.AsData();

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

                target.AddLaptime(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.LaptimeSeconds).IsNull();
                Check.That(target.IsDirty).IsFalse();
            }

        }

        public class TyresWear
        {
            private FakePitWallConfiguration _configuration;

            public TyresWear()
            {
                _configuration = new FakePitWallConfiguration();
            }
            public LiveAggregator GetTarget()
            {
                return new LiveAggregator(_configuration);
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
                Check.That(actual.TyresWear.FrontLeftWear).IsEqualTo(85.000000001);
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
                Check.That(actual.TyresWear.FrontLeftWear).IsNull();
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
                Check.That(actual.TyresWear.FrontRightWear).IsEqualTo(85.000000001);
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
                Check.That(actual.TyresWear.FrontRightWear).IsNull();

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
                Check.That(actual.TyresWear.ReartLeftWear).IsEqualTo(85.000000001);
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
                Check.That(actual.TyresWear.ReartLeftWear).IsNull();
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

            // ===== Tyre wear rear right

            [Fact]
            public void GIVEN_rearRightTyreWear_isNotNull_THEN_data_rearRightTyreWear_isNotNull()
            {
                // ARRANGE
                double original = 85.000000001;

                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreWear(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresWear.RearRightWear).IsEqualTo(85.000000001);
                Check.That(target.IsDirty).IsTrue();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearRightTyreWear_isNull_WHEN_tyreWear_isNull()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreWear(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresWear.ReartLeftWear).IsNull();
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearRightTyreValue_isNull_THEN_isDirty_isFalse()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreWear(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            // ===== Tyre wear rear right  ___EOF___
        }

        public class TyresTemperature
        {
            private FakePitWallConfiguration _configuration;

            public TyresTemperature()
            {
                _configuration = new FakePitWallConfiguration();
            }
            public LiveAggregator GetTarget()
            {
                return new LiveAggregator(_configuration);
            }

            // ===== Tyre temperature front left

            [Fact]
            public void GIVEN_frontLeftTyreTemp_isNotNull_THEN_data_frontLeftTyreTemp_isNotNull()
            {
                // ARRANGE
                double original = 85.000000001;

                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontLeftTyreTemperature(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.FrontLeftTemp).IsEqualTo(85.000000001);
                Check.That(target.IsDirty).IsTrue();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_frontLeftTyreTemp_isNull_WHEN_frontLeftTyreTemp_isNull()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontLeftTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.FrontLeftTemp).IsNull();
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_frontLeftTyreTempValue_isNull_THEN_isDirty_isFalse()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontLeftTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            // ===== Tyre temperature front left ___EOF___

            // ===== Tyre temperature front right

            [Fact]
            public void GIVEN_frontRighTyreTemp_isNotNull_THEN_data_frontRightTyreTemp_isNotNull()
            {
                // ARRANGE
                double original = 85.000000001;

                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontRightTyreTemperature(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.FrontRightTemp).IsEqualTo(85.000000001);
                Check.That(target.IsDirty).IsTrue();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_frontRightTyreTemp_isNull_WHEN_frontRightTyreTemp_isNull()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontRightTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.FrontLeftTemp).IsNull();
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_frontRightTyreTempValue_isNull_THEN_isDirty_isFalse()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddFrontRightTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            // ===== Tyre temperature front right ___EOF___

            // ===== Tyre temperature rear left

            [Fact]
            public void GIVEN_rearLeftyreTemp_isNotNull_THEN_data_rearLeftTyreTemp_isNotNull()
            {
                // ARRANGE
                double original = 85.000000001;

                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearLeftTyreTemperature(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.RearLeftTemp).IsEqualTo(85.000000001);
                Check.That(target.IsDirty).IsTrue();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearLeftTyreTemp_isNull_WHEN_rearLeftTyreTemp_isNull()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearLeftTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.RearLeftTemp).IsNull();
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearLeftTyreTempValue_isNull_THEN_isDirty_isFalse()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearLeftTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            // ===== Tyre temperature rear left ___EOF___

            // ===== Tyre temperature rear right

            [Fact]
            public void GIVEN_rearRightTyreTemp_isNotNull_THEN_data_rearRightTyreTemp_isNotNull()
            {
                // ARRANGE
                double original = 85.000000001;

                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreTemperature(original);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.RearRightTemp).IsEqualTo(85.000000001);
                Check.That(target.IsDirty).IsTrue();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearRightTyreTemp_isNull_WHEN_rearRightTyreTemp_isNull()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreTemperature(null);

                watch.Stop();

                var actual = target.AsData();

                // ASSERT
                Check.That(actual.TyresTemperatures.RearRightTemp).IsNull();
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            [Fact]
            public void GIVEN_rearRightTyreTempValue_isNull_THEN_isDirty_isFalse()
            {
                // ARRANGE
                var target = GetTarget();

                // ACT
                Stopwatch watch = Stopwatch.StartNew();

                target.AddRearRightTyreTemperature(null);

                watch.Stop();

                // ASSERT
                Check.That(target.IsDirty).IsFalse();

                Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
            }

            // ===== Tyre temperature rear right ___EOF___
        }

        public class WeatherCondition
        {
            private FakePitWallConfiguration _configuration;

            public WeatherCondition()
            {
                _configuration = new FakePitWallConfiguration();
            }
            public LiveAggregator GetTarget()
            {
                return new LiveAggregator(_configuration);
            }

            [Fact]
            public void GIVEN_avgWetness_isNull_THEN_avgWetness_isNull_AND_isDirty_isFalse()
            {
                var target = GetTarget();

              target.EnsureValueNullMapped(
                    a => a.AddAvgWetness(null),
                    d => d.AvgWetness);

                Check.That(target.IsDirty).IsFalse();
            }

            [Fact]
            public void GIVEN_avgWetness_isNotNull_THEN_avgWetness_isNotNull_AND_isDirty_isTrue()
            {
                var target = GetTarget();

                target.EnsureValueNotNullMapped(
                    a => a.AddAvgWetness(10.0),
                    d => d.AvgWetness);

                Check.That(target.IsDirty).IsTrue();
            }

            [Fact]
            public void GIVEN_avgWetness_isSet_THEN_avgWetness_isEqual_toExpected_AND_isDirty_isTrue()
            {
                var target = GetTarget();

                target.EnsureValueEqualsExpected(
                    a => a.AddAvgWetness(10.0),
                    d => d.AvgWetness,
                    10.0);

                Check.That(target.IsDirty).IsTrue();
            }



            [Fact]
            public void GIVEN_airTemp_isNull_THEN_airTemp_isNull_AND_isDirty_isFalse()
            {
                var target = GetTarget();

                target.EnsureValueNullMapped(
                      a => a.SetAirTemperature(null),
                      d => d.AirTemperature);

                Check.That(target.IsDirty).IsFalse();
            }

            [Fact]
            public void GIVEN_airTemp_isNotNull_THEN_airTemp_isNotNull_AND_isDirty_isTrue()
            {
                var target = GetTarget();

                target.EnsureValueNotNullMapped(
                    a => a.SetAirTemperature(10.0),
                    d => d.AirTemperature);

                Check.That(target.IsDirty).IsTrue();
            }

            [Fact]
            public void GIVEN_airTemp_isSet_THEN_airTemp_isEqual_toExpected_AND_isDirty_isTrue()
            {
                var target = GetTarget();

                target.EnsureValueEqualsExpected(
                    a => a.AddAvgWetness(10.0),
                    d => d.AvgWetness,
                    10.0);

                Check.That(target.IsDirty).IsTrue();
            }

        }
        
      

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
            Check.That(actual.LaptimeSeconds).IsNull();

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

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_tyresFrontLeft_is_null()
        {
            // ARRANGE
            double original = 10.0;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontLeftTyreWear(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.TyresWear.FrontLeftWear).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_tyresFrontRight_is_null()
        {
            // ARRANGE
            double original = 10.0;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddFrontRightTyreWear(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.TyresWear.FrontRightWear).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_tyresRearLeft_is_null()
        {
            // ARRANGE
            double original = 10.0;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddRearLeftTyreWear(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.TyresWear.ReartLeftWear).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        [Fact]
        public void Given_aggregator_cleared_THEN_isDirty_is_false_AND_tyresRearRight_is_null()
        {
            // ARRANGE
            double original = 10.0;

            var target = GetTarget();

            // ACT
            Stopwatch watch = Stopwatch.StartNew();

            target.AddRearRightTyreWear(original);

            target.Clear();

            var actual = target.AsData();

            watch.Stop();

            // ASSERT
            Check.That(target.IsDirty).IsFalse();
            Check.That(actual.TyresWear.RearRightWear).IsNull();

            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
    }

    public static class LiveAggregatorTestingExtensions
    {
        public static void EnsureValueNullMapped(
          this LiveAggregator target,
          Action<ILiveAggregator> setDataAction,
          Func<IData, double?> fieldSelector)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(fieldSelector(actual)).IsNull();
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        public static void EnsureValueNotNullMapped(
           this LiveAggregator target,
           Action<ILiveAggregator> setDataAction,
           Func<IData, double?> fieldSelector)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = target.AsData();

            // ASSERT
            Check.That(fieldSelector(actual)).IsNotNull();
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        public static void EnsureValueEqualsExpected(
            this LiveAggregator target,
             Action<ILiveAggregator> setDataAction,
               Func<IData, double?> fieldSelector,
               double? expected)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = target.AsData();

            Check.That(fieldSelector(actual)).IsEqualTo(expected);
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
    }
}
