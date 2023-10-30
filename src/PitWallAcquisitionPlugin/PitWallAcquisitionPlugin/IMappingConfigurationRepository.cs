using PitWallAcquisitionPlugin.Aggregations.Mappers;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin
{
    public interface IMappingConfigurationRepository : IEnumerable<ILiveMapper>
    {
    }
}