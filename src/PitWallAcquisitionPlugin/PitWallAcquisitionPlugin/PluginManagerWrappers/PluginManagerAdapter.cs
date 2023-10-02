﻿using SimHub.Plugins;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers
{
    public sealed class PluginManagerAdapter : IPluginManagerAdapter
    {
        private readonly PluginManager _pluginManager;

        public PluginManagerAdapter(PluginManager pluginManager)
        {
            _pluginManager = pluginManager;
        }

        public object GetPropertyValue(string key)
        {
            return _pluginManager.GetPropertyValue(key);
        }
    }
}
