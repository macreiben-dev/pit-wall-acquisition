
using System;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;

namespace PitWallAcquisitionPlugin.Logging
{
    internal sealed class SimhubLogger : ILogger
    {
        private const string LOG_PREFIX = "PitWallAcquisitionPlugin: ";

        public void Debug(string message)
        {
            SimHub.Logging.Current.Debug(Format(message));
        }

        public void Error(string message)
        {
            SimHub.Logging.Current.Error(Format(message));
        }

        public void Error(string message, Exception ex)
        {
            SimHub.Logging.Current.Error(Format(message));
        }

        public void Info(string message)
        {
            SimHub.Logging.Current.Info(Format(message));
        }

        public void Warn(string message)
        {
            SimHub.Logging.Current.Warn(Format(message));
        }

        private string Format(string message)
        {
            return LOG_PREFIX + message;
        }
    }
}
