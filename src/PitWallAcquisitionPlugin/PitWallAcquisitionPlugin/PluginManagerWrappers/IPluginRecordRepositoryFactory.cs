using SimHub.Plugins;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    internal interface IPluginRecordRepositoryFactory
    {
        IPluginRecordRepository GetInstance(PluginManager pluginManager);
    }
}