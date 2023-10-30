using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Mappers;
using System;

namespace PitWallAcquisitionPlugin.Aggregations.Mappers
{
    public sealed class LiveMapperFactory
    {
        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, double?> sourceSelector, Action<ILiveAggregator, double?> setter)
        {
            return new LiveMapper<double?>(sourceSelector, setter);
        }


        public static ILiveMapper GetInstance(Func<IPluginRecordRepository, string> sourceSelector, Action<ILiveAggregator, string> setter)
        {
            return new LiveMapper<string>(sourceSelector, setter);
        }
    }
}
