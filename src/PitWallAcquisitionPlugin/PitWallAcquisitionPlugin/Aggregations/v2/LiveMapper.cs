using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.v2
{
    //public class LiveAggregator2 : ILiveAggregator2
    //{
    //}

    //public interface ILiveAggregator2
    //{

    //}

    public sealed class LiveMapper : ILiveMapper
    {
        private readonly Func<IPluginRecordRepository, double?> sourceSelector;
        private readonly Action<ILiveAggregator, double?> setter;

        private readonly Func<IPluginRecordRepository, string> sourceSelectorString;
        private readonly Action<ILiveAggregator, string> setterString;

        public LiveMapper(Func<IPluginRecordRepository, double?> sourceSelector, Action<ILiveAggregator, double?> setter)
        {
            // Note : this is coupled on counter type.

            this.sourceSelector = sourceSelector;
            this.setter = setter;
        }

        public LiveMapper(Func<IPluginRecordRepository, string> sourceSelector, Action<ILiveAggregator, string> setter)
        {
            // Note : this is coupled on counter type.

            this.sourceSelectorString = sourceSelector;
            this.setterString = setter;
        }

        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, double?> sourceSelector, Action<ILiveAggregator, double?> setter)
        {
            return new LiveMapper<double?>(sourceSelector, setter);
        }


        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, string> sourceSelector, Action<ILiveAggregator, string> setter)
        {
            return new LiveMapper<string>(sourceSelector, setter);
        }

        public void Set(IPluginRecordRepository adapter, ILiveAggregator aggregator)
        {
            // Note : this is coupled on counter type.

            if (adapter is null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            var data = sourceSelector(adapter);

            setter(aggregator, data);
        }
    }

    public sealed class LiveMapper<TCounter> : ILiveMapper
    {
        private readonly Func<IPluginRecordRepository, TCounter> sourceSelector;
        private readonly Action<ILiveAggregator, TCounter> setter;

        public LiveMapper(Func<IPluginRecordRepository, TCounter> sourceSelector, Action<ILiveAggregator, TCounter> setter)
        {
            this.sourceSelector = sourceSelector;
            this.setter = setter;
        }

        public void Set(IPluginRecordRepository adapter, ILiveAggregator aggregator)
        {
            if (adapter is null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }

            if (aggregator is null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            var data = sourceSelector(adapter);

            setter(aggregator, data);
        }
    }




    public class DummyApiForwarderService
    {
        public void Update()
        {
            var allConfiguration = new[] {
                new LiveMapper(s => s.AirTemperature, (a, data) => a.SetAirTemperature(data)),
                new LiveMapper(s => s.AvgRoadWetness, (a, data) => a.SetAvgWetness(data))
            };
        }
    }
}
