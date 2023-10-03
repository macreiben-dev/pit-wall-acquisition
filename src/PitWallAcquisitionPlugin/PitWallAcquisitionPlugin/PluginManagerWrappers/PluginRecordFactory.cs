using FuelAssistantMobile.DataGathering.SimhubPlugin;
using SimHub.Plugins;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    public sealed class PluginRecordFactory : IPluginRecordFactory
    {
        public IPluginRecordRepository GetInstance(PluginManager pluginManager)
        {
            return new PluginManagerWrapper(new PluginManagerAdapter(pluginManager));
        }
    }
}
