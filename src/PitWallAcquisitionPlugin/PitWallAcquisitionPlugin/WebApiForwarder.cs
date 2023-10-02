using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using FuelAssistantMobile.DataGathering.SimhubPlugin.PluginManagerWrappers;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using GameReaderCommon;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using SimHub.Plugins;
using System.Timers;

namespace PitWallAcquisitionPlugin
{
    [PluginDescription("Broadcast data to a remote API to work on race strategy.")]
    [PluginAuthor("Christian \"MacReiben\" Finel")]
    [PluginName("Fam Data Plugin")]
    public sealed partial class WebApiForwarder : IDataPlugin
    {
        private readonly IPluginRecordFactory _pluginRecordFactory;
        private readonly ILogger _logger;
        private readonly WebApiForwarderService _webApiForwarderService;

        public WebApiForwarder()
            : this(
                  new SimhubLogger(),
                  new LiveAggregator(),
                  new FamRemoteRepository(),
                  new PluginRecordFactory())
        {

        }

        public WebApiForwarder(
            ILogger logger,
            ILiveAggregator aggregator,
            IStagingDataRepository dataRepository,
            IPluginRecordFactory pluginRecordFactory)
        {
            _logger = logger;

            _pluginRecordFactory = pluginRecordFactory;

            _webApiForwarderService = new WebApiForwarderService(
                aggregator,
                dataRepository,
                logger,
                10,
                5000);
        }

        // ===========================================================

        private PluginManager _pluginManager;

        public PluginManager PluginManager { set => _pluginManager = value; }

        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            // THOUGHT: add call to service here to grab the data we want from plugin manager
            IPluginRecordRepository pluginRecordRepository
                = _pluginRecordFactory.GetInstance(pluginManager);

            _webApiForwarderService.HandleDataUpdate(pluginRecordRepository);
        }

        public void End(PluginManager pluginManager)
        {
            _logger.Info("Data plugin closing ...");

            _webApiForwarderService.Stop();

            _logger.Info("Data plugin closing DONE!");
        }

        public void Init(PluginManager pluginManager)
        {
            _logger.Info("Starting Fam Data Gathering plugin ...");

            _webApiForwarderService.Start();

            _logger.Info("Starting Fam Data Gathering plugin DONE!");
        }

        // ===========================================================
    }
}
