using FuelAssistantMobile.DataGathering.SimhubPlugin;
using SimHub.Plugins;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    public interface IPluginRecordRepositoryFactory
    {
        IPluginRecordRepository GetInstance(PluginManager pluginManager);
    }
}