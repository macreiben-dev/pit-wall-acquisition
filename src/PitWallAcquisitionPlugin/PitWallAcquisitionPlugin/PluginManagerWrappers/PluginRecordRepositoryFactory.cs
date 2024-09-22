using SimHub.Plugins;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    internal sealed class PluginRecordRepositoryFactory : IPluginRecordRepositoryFactory
    {
        public IPluginRecordRepository GetInstance(PluginManager pluginManager)
        {
            return new PluginManagerWrapper(new PluginManagerAdapter(pluginManager));
        }
    }
}
