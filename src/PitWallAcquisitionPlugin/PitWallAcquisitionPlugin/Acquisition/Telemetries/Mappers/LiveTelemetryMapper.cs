using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{

    public sealed class LiveTelemetryMapper<TCounter> : ILiveTelemetryMapper
    {
        private readonly GenericLiveMapper<TCounter, ITelemetryLiveAggregator> _mapper;
        /**
         * THOUGHT: We have some genericity here that could make this code reusable.
         * */

        public LiveTelemetryMapper(Func<IPluginRecordRepository, TCounter> sourceSelector, Action<ITelemetryLiveAggregator, TCounter> setter)
        {
            _mapper = new GenericLiveMapper<TCounter, ITelemetryLiveAggregator>(sourceSelector, setter);
        }

        public void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator)
        {
            _mapper.Set(adapter, aggregator);
        }
    }
}
