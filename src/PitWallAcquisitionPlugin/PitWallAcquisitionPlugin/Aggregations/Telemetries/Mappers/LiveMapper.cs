using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{

    public sealed class LiveMapper<TCounter> : ILiveMapper
    {
        private readonly Func<IPluginRecordRepository, TCounter> sourceSelector;
        private readonly Action<ITelemetryLiveAggregator, TCounter> setter;

        public LiveMapper(Func<IPluginRecordRepository, TCounter> sourceSelector, Action<ITelemetryLiveAggregator, TCounter> setter)
        {
            this.sourceSelector = sourceSelector ?? throw new ArgumentNullException(nameof(sourceSelector));

            this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator)
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
}
