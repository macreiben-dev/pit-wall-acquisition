using System;

namespace PitWallAcquisitionPlugin.PluginManagerWrappers
{
    public static class PluginManagerFieldConverter
    {
        public static string ToString(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return null;
            }

            return data.ToString();
        }

        public static double? ToDouble(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return null;
            }

            return Convert.ToDouble(data);
        }

        public static bool ToBoolean(string key, IPluginManagerAdapter pluginManager)
        {
            var data = pluginManager.GetPropertyValue(key);

            if (data == null)
            {
                return false;
            }

            return Convert.ToBoolean(data);
        }
    }
}