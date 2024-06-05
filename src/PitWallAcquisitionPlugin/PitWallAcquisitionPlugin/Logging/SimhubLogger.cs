using System;
using log4net;

namespace PitWallAcquisitionPlugin.Logging
{
    internal sealed class SimhubLogger : ILogger
    {
        private readonly ILog _log;
        private readonly IConditionalLogger _debugLogger;
        private readonly IConditionalLogger _infoLogger;
        private readonly IConditionalLogger _warnLogger;
        private readonly IConditionalLogger _errorLogger;
        private const string LOG_PREFIX = "PitWallAcquisitionPlugin: ";

        public SimhubLogger(ILog log,
            IConditionalLoggerFactory loggerFactory)
        {
            _log = log;
            _debugLogger = loggerFactory.CreateLogger();
            _infoLogger = loggerFactory.CreateLogger();
            _warnLogger = loggerFactory.CreateLogger();
            _errorLogger = loggerFactory.CreateLogger();
        }

        public void Debug(string message)
        {
            _debugLogger.Log(Format(message), _log.Debug);
        }

        public void Error(string message)
        {
            _errorLogger.Log(Format(message), _log.Error);
        }

        public void Error(string message,
            Exception ex)
        {
            _errorLogger.Log(Format(message), msg => _log.Error(msg, ex));
        }

        public void Info(string message)
        {
            _infoLogger.Log(Format(message), _log.Info);
        }

        public void Warn(string message)
        {
            _warnLogger.Log(Format(message), _log.Warn);
        }

        private string Format(string message)
        {
            return LOG_PREFIX + message;
        }
    }
}