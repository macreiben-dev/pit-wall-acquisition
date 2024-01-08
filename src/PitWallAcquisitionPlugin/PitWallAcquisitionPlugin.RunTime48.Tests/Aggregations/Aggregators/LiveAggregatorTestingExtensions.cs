using NFluent;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using System;
using System.Diagnostics;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Aggregators
{
    public static class LiveAggregatorTestingExtensions
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
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }

        public static void EnsureValueNotNullMapped(
           this TelemetryLiveAggregator target,
           Action<ITelemetryLiveAggregator> setDataAction,
           Func<ITelemetryData, double?> fieldSelector)
        {
            Stopwatch watch = Stopwatch.StartNew();

            setDataAction(target);

            watch.Stop();

            var actual = (ITelemetryData)target.AsData();

            // ASSERT
            Check.That(fieldSelector(actual)).IsNotNull();
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
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
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
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
            Check.That(watch.ElapsedMilliseconds).IsLessOrEqualThan(3);
        }
    }
}
