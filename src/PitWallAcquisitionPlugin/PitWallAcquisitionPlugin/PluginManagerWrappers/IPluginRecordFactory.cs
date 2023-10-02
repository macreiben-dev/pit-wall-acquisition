using SimHub.Plugins;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers
{
    public interface IPluginRecordFactory
    {
        IPluginRecordRepository GetInstance(PluginManager pluginManager);
    }
}