using PitWallAcquisitionPlugin.Aggregations.v2;
using System.Collections.Generic;

namespace PitWallAcquisitionPlugin
{
    public interface IMappingConfigurationRepository : IEnumerable<ILiveMapper>
    {
    }
}