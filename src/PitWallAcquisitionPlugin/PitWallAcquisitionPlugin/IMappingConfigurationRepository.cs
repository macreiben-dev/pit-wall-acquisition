using PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin
{
    internal interface IMappingConfigurationRepository : IEnumerable<ILiveTelemetryMapper>
    {
    }
}