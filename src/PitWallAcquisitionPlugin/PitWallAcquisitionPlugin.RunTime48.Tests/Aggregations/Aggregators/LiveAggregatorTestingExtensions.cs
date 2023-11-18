using NFluent;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using System;
using System.Diagnostics;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Aggregators
{
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

        public static void EnsureValueEqualsExpected<TExpected>(
           this LiveAggregator target,
            Action<ILiveAggregator> setDataAction,
              Func<IData, TExpected> fieldSelector,
              TExpected expected)
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
