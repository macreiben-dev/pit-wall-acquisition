﻿using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers
{
    internal interface ILiveTelemetryMapper
    {
        void Set(IPluginRecordRepository adapter, ITelemetryLiveAggregator aggregator);
    }
}