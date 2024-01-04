using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{
    public sealed class LiveMapperFactory
    {
        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, double?> sourceSelector, Action<ITelemetryLiveAggregator, double?> setter)
        {
            return new LiveMapper<double?>(sourceSelector, setter);
        }


        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, string> sourceSelector, Action<ITelemetryLiveAggregator, string> setter)
        {
            return new LiveMapper<string>(sourceSelector, setter);
        }
    }
}
