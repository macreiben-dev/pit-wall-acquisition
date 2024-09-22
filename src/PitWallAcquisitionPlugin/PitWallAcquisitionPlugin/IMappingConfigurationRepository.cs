using System.Collections.Generic;
using PitWallAcquisitionPlugin.Acquisition.Telemetries.Mappers;

namespace PitWallAcquisitionPlugin
{
    internal interface IMappingConfigurationRepository : IEnumerable<ILiveTelemetryMapper>
    {
    }
}