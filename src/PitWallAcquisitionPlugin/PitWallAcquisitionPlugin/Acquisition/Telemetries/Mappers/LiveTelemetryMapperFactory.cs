using System;
using PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin.Acquisition.Telemetries.Mappers
{
    internal sealed class LiveTelemetryMapperFactory
    {
        public static ILiveTelemetryMapper GetInstance(Func<IPluginRecordRepository, double?> sourceSelector, Action<ITelemetryLiveAggregator, double?> setter)
        {
            return new LiveTelemetryMapper<double?>(sourceSelector, setter);
        }


        public static ILiveTelemetryMapper GetInstance(Func<IPluginRecordRepository, string> sourceSelector, Action<ITelemetryLiveAggregator, string> setter)
        {
            return new LiveTelemetryMapper<string>(sourceSelector, setter);
        }
    }
}
