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

    public sealed class LiveMapper
    {
        private readonly Func<IPluginRecordRepository, double?> sourceSelector;
        private readonly Action<ILiveAggregator, double?> setter;

        public LiveMapper(Func<IPluginRecordRepository, double?> sourceSelector, Action<ILiveAggregator, double?> setter)
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
