using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using PitWallAcquisitionPlugin.Aggregations;
using System.Timers;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin
{
    public sealed class WebApiForwarderService
    {
        private const int Frequency = 10; // 10Hz
        private int _internalErrorCount = 0;
        private bool _notifiedStop = false;
        private bool _firstLaunch = false;

        private Timer _postTimer;
        private Timer _autoReactivate;

        private readonly IStagingDataRepository _dataRepository;
        private readonly ILiveAggregator _liveAggregator;
        private readonly ILogger _logger;

        public WebApiForwarderService(
            ILiveAggregator aggregator,
            IStagingDataRepository dataRepository,
            ILogger logger,
            double postToApiTimerHz,
            int autoReactivateTimer)
        {
            _postTimer = new Timer(1000 / postToApiTimerHz); // Interval in milliseconds for 10Hz (1000ms / 10Hz = 100ms)
            _postTimer.Elapsed += PostData;

            _autoReactivate = new Timer(autoReactivateTimer);
            _autoReactivate.Elapsed += AutoReactivate;

            _dataRepository = dataRepository;

            _liveAggregator = aggregator;
            _logger = logger;

            _notifiedStop = true;
        }

        public void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository)
        {
            if (!_firstLaunch && pluginRecordRepository.IsGameRunning)
            {
                Start();

                UpdateAggregator(pluginRecordRepository);

                _firstLaunch = true;
            }

            if (!pluginRecordRepository.IsGameRunning)
            {
                Stop();

                return;
            }

            if (pluginRecordRepository.IsGameRunning)
            {
                Start();

                UpdateAggregator(pluginRecordRepository);

                return;
            }
        }

        public void Start()
        {
            if (_notifiedStop == false)
            {
                return;
            }

            _internalErrorCount = 0;
            _notifiedStop = false;
            _postTimer.Start();
            _liveAggregator.Clear();

            _logger.Info("Fam Data Gathering plugin STARTED");
        }

        public void Stop()
        {
            if (_notifiedStop == true)
            {
                return;
            }

            _liveAggregator.Clear();
            _postTimer.Stop();
            _internalErrorCount = 0;
            _notifiedStop = true;

            _logger.Info("Fam Data Gathering plugin STOPPED");
        }

        private async void PostData(object sender, ElapsedEventArgs e)
        {
            // THOUGHT: check game status before doing anything. If it is not running. Then do nothing.
            if (ShouldStopTimer())
            {
                try
                {
                    Stop();
                }
                catch { }

                _logger.Error("WebAPI post stoped.");

                return;
            }

            if (ShouldNotifyRetrying())
            {
                _logger.Warn($"Retrying to contact API - error count is [{_internalErrorCount}]");
            }

            if (!_liveAggregator.IsDirty)
            {
                return;
            }

            // Replace the following lines with your own logic to get the data you want to send
            var dataToSend = new DataVessel
            {
                data = _liveAggregator.AsData()
            };

            try
            {
                await _dataRepository.SendAsync(dataToSend);
            }
            catch (ErrorWhenSendDataException ex)
            {
                _internalErrorCount++;

                _logger.Error($"Issue during posting [{ex.WebApiUrl}] - [{_internalErrorCount}] error count.");
                _logger.Error("Posted data is:");
                _logger.Error(ex.JsonData);
                _logger.Error($"Exception is:", ex);
            }
            catch (StatusCodeNotOkException ex)
            {
                _internalErrorCount++;

                _logger.Error($"Issue during posting [{ex.WebApiUrl}] - [{_internalErrorCount}] error count.");
                _logger.Error($"API failed returned code is not OK - [{ex.StatusCode}]");
            }
        }

        private void AutoReactivate(object sender, ElapsedEventArgs e)
        {
            if (_notifiedStop && _internalErrorCount >= 3)
            {
                _logger.Info("Trying to restart plugin after errors ...");

                Start();
            }
        }

        private void UpdateAggregator(IPluginRecordRepository racingDataRepository)
        {
            var gameDataSessionTimeLeft = racingDataRepository.SessionTimeLeft;

            _liveAggregator.AddSessionTimeLeft(gameDataSessionTimeLeft);

        }

        private bool ShouldStopTimer()
        {
            return _internalErrorCount >= 3;
        }

        private bool ShouldNotifyRetrying()
        {
            return _internalErrorCount > 0 && _internalErrorCount < 3;
        }
    }
}
