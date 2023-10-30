using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Mappers
{

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
}
