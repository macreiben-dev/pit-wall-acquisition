using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using System.Timers;

namespace PitWallAcquisitionPlugin
{
    public sealed class WebApiForwarderService : IWebApiForwarderService
    {
        private int _internalErrorCount = 0;
        private bool _notifiedStop = false;
        private bool _firstLaunch = false;

        private readonly Timer _postTimer;
        private readonly Timer _autoReactivate;

        private readonly IStagingDataRepository _dataRepository;
        private readonly IMappingConfigurationRepository _mappingConfiguration;
        private readonly ITelemetryLiveAggregator _liveAggregator;
        private readonly ILogger _logger;

        /// <summary>
        /// Forwards data to the vortext API.
        /// </summary>
        /// <param name="aggregator">The aggregator of data.</param>
        /// <param name="dataRepository">The repository to send data to the API.</param>
        /// <param name="mappingConfiguration"></param>
        /// <param name="logger">The logger</param>
        /// <param name="postToApiTimerHz">Post to API frequency</param>
        /// <param name="autoReactivateTimer"></param>
        public WebApiForwarderService(
            ITelemetryLiveAggregator aggregator,
            IStagingDataRepository dataRepository,
            IMappingConfigurationRepository mappingConfiguration,
            ILogger logger,
            double postToApiTimerHz, int autoReactivateTimer)
        {
            _postTimer = new Timer(1000 / postToApiTimerHz); // Interval in milliseconds for 10Hz (1000ms / 10Hz = 100ms)
            _postTimer.Elapsed += PostData;

            _autoReactivate = new Timer(autoReactivateTimer);
            _autoReactivate.Elapsed += AutoReactivate;

            _dataRepository = dataRepository;
            _mappingConfiguration = mappingConfiguration;
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
            var dataToSend = _liveAggregator.AsData();

            try
            {
                if (EnsureSimerKeyAndPilotNameAreSet(dataToSend))
                {
                    _logger.Error($"Mandatory configuration missing, PilotName is [{dataToSend.PilotName}] - SimerKey is [{dataToSend.SimerKey}]");

                    return;
                }

                await _dataRepository.SendAsync(dataToSend);

                // Reset to 0 after one success.
                _internalErrorCount = 0;
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

        private static bool EnsureSimerKeyAndPilotNameAreSet(ITelemetryData dataToSend)
        {
            return string.IsNullOrEmpty(dataToSend.PilotName)
                || string.IsNullOrEmpty(dataToSend.SimerKey);
        }

        private void AutoReactivate(object sender, ElapsedEventArgs e)
        {
            if (_notifiedStop && _internalErrorCount >= 3)
            {
                _logger.Info("Trying to restart plugin after errors ...");

                Start();
            }
        }

        private void UpdateAggregator(
            IPluginRecordRepository racingDataRepository)
        {

            /**
            * Idea: we have one side where we read from plugin manager, and another 
            * in which we map the retrieved data to the aggregator.
            * 
            * I don't like big dictionary cause I like control. But I might have to
            * centralize the definition of the copy from plugin manager to racing data repo.
            * 
            * */

            foreach (var config in _mappingConfiguration)
            {
                config.Set(racingDataRepository, _liveAggregator);
            }
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
