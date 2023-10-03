using FuelAssistantMobile.DataGathering.SimhubPlugin;
using SimHub.Plugins;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    public interface IPluginRecordFactory
    {
        IPluginRecordRepository GetInstance(PluginManager pluginManager);
    }
}