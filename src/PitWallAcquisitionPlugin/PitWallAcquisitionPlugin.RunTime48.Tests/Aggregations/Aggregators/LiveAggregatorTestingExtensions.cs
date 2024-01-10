using NFluent;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using System;
using System.Diagnostics;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Aggregators
{
    internal static class LiveAggregatorTestingExtensions
    {
        public static void EnsureValueNullMapped(
          this TelemetryLiveAggregator target,
          Action<ITelemetryLiveAggregator> setDataAction,
          Func<ITelemetryData, double?> fieldSelector)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            // ASSERT
            Check.That(fieldSelector(actual)).IsNull();
            EnsureExecutionTakesNotLong(watch);
        }

        public static void EnsureValueNotNullMapped(
           this TelemetryLiveAggregator target,
           Action<ITelemetryLiveAggregator> setDataAction,
           Func<ITelemetryData, double?> fieldSelector)
        {
            var watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            // ASSERT
            Check.That(fieldSelector(actual)).IsNotNull();
            EnsureExecutionTakesNotLong(watch);
        }

        public static void EnsureValueEqualsExpected(
            this TelemetryLiveAggregator target,
             Action<ITelemetryLiveAggregator> setDataAction,
               Func<ITelemetryData, double?> fieldSelector,
               double? expected)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            Check.That(fieldSelector(actual)).IsEqualTo(expected);
            EnsureExecutionTakesNotLong(watch);
        }

        public static void EnsureValueEqualsExpected<TExpected>(
           this TelemetryLiveAggregator target,
            Action<ITelemetryLiveAggregator> setDataAction,
              Func<ITelemetryData, TExpected> fieldSelector,
              TExpected expected)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            Check.That(fieldSelector(actual)).IsEqualTo(expected);
            EnsureExecutionTakesNotLong(watch);
        }

        private static void EnsureExecutionTakesNotLong(Stopwatch watch)
        {
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(4);
        }

    }
}
