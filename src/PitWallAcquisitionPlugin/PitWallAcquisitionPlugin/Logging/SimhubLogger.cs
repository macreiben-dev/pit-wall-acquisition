
using System;
using log4net;

namespace PitWallAcquisitionPlugin.Logging
{
    internal sealed class SimhubLogger : ILogger
    {
        private readonly ILog _log;
        private const string LOG_PREFIX = "PitWallAcquisitionPlugin: ";

        public SimhubLogger(ILog log)
        {
            _log = log;
        }
        
        public void Debug(string message)
        {
            _log.Debug(Format(message));
        }
        
        public void Error(string message)
        {
            _log.Error(Format(message));
        }

        public void Error(string message, Exception ex)
        {
            _log.Error(Format(message), ex);
        }

        public void Info(string message)
        {
            _log.Info(Format(message));
        }

        public void Warn(string message)
        {
            _log.Warn(Format(message));
        }

        private string Format(string message)
        {
            return LOG_PREFIX + message;
        }
    }
}
