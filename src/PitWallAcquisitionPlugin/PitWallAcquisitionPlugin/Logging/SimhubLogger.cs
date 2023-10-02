
using System;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Logging
{
    public class SimhubLogger : ILogger
    {
        public void Debug(string message)
        {
            SimHub.Logging.Current.Debug(message);
        }

        public void Error(string message)
        {
            SimHub.Logging.Current.Error(message);
        }

        public void Error(string message, Exception ex)
        {
            SimHub.Logging.Current.Error(message, ex);
        }

        public void Info(string message)
        {
            SimHub.Logging.Current.Info(message);
        }

        public void Warn(string message)
        {
            SimHub.Logging.Current.Warn(message);
        }
    }
}
