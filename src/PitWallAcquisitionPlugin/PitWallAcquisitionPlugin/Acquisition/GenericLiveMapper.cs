using FuelAssistantMobile.DataGathering.SimhubPlugin;
using System;

namespace PitWallAcquisitionPlugin.Aggregations
{

    public sealed class GenericLiveMapper<TCounter, ISourceAggregator> //: ILiveMapper
    {
        private readonly Func<IPluginRecordRepository, TCounter> sourceSelector;
        private readonly Action<ISourceAggregator, TCounter> setter;

        /**
         * THOUGHT: We have some genericity here that could make this code reusable.
         * */

        public GenericLiveMapper(Func<IPluginRecordRepository, TCounter> sourceSelector, Action<ISourceAggregator, TCounter> setter)
        {
            this.sourceSelector = sourceSelector ?? throw new ArgumentNullException(nameof(sourceSelector));

            this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }

        public void Set(IPluginRecordRepository adapter, ISourceAggregator aggregator)
        {
            if (adapter is null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }

            if (aggregator == null)
            {
                throw new ArgumentNullException(nameof(aggregator));
            }

            var data = sourceSelector(adapter);

            setter(aggregator, data);
        }
    }
}
