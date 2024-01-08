using PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin
{
    public interface IMappingConfigurationRepository : IEnumerable<ILiveTelemetryMapper>
    {
    }
}