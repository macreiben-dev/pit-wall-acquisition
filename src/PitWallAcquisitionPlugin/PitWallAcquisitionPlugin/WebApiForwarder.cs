using Autofac;
using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using GameReaderCommon;
using PitWallAcquisitionPlugin.Acquisition;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Leadeboards;
using PitWallAcquisitionPlugin.Aggregations.Telemetries;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories;
using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.HealthChecks.Repositories;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using PitWallAcquisitionPlugin.Repositories;
using PitWallAcquisitionPlugin.Tests.UI.Commands;
using PitWallAcquisitionPlugin.UI.ViewModels;
using PitWallAcquisitionPlugin.UI.Views;
using SimHub.Plugins;
using System.Windows.Controls;

namespace PitWallAcquisitionPlugin
{
    [PluginDescription("Broadcast data to a remote API to work on race strategy.")]
    [PluginAuthor("Christian \"MacReiben\" Finel")]
    [PluginName("Pitwall Acquisition Plugin")]
    public sealed partial class WebApiForwarder : IDataPlugin, IWPFSettings
    {
        private readonly IPluginRecordRepositoryFactory _pluginRecordFactory;
        private readonly IContainer _builder;
        private readonly ILogger _logger;
        private readonly ITelemetryForwarderService _webApiForwarderService;
        private readonly IAcquisitionService _acquisitionService;

        public WebApiForwarder()
            : this(
                new SimhubLogger(),
                new PluginRecordRepositoryFactory())
        {

        }

        public WebApiForwarder(
            ILogger logger,
            IPluginRecordRepositoryFactory pluginRecordFactory)
        {
            IContainer builder = CreateBuilder(
                logger,
                this);

            _builder = builder;

            _logger = _builder.Resolve<ILogger>();

            _pluginRecordFactory = _builder.Resolve<IPluginRecordRepositoryFactory>();

            _webApiForwarderService = _builder.Resolve<ITelemetryForwarderService>();

            _acquisitionService = _builder.Resolve<IAcquisitionService>();
        }

        private static IContainer CreateBuilder(
            ILogger logger,
            IDataPlugin plugin)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(plugin).As<IPlugin>();

            containerBuilder.RegisterInstance(logger);

            containerBuilder.RegisterType<TelemetryLiveAggregator>()
                .As<ITelemetryLiveAggregator>()
                .SingleInstance();

            //-------------

            containerBuilder.RegisterType<RemotesRepository>()
                .As<IRemotesRepository>()
                .SingleInstance();

            //-------------

            containerBuilder.RegisterType<HealthCheckService>()
                .As<IHealthCheckService>();

            containerBuilder.RegisterType<HealthCheckRepository>()
                .As<IHealthCheckRepository>()
                .SingleInstance();

            containerBuilder.RegisterType<PluginRecordRepositoryFactory>()
                .As<IPluginRecordRepositoryFactory>()
                .SingleInstance();

            containerBuilder.RegisterType<WebApiTelemetryForwarderService>()
                .As<ITelemetryForwarderService>()
                .WithParameter("postToApiTimerHz", 1)
                .WithParameter("autoReactivateTimer", 5000)
                .SingleInstance();

            containerBuilder.RegisterType<PluginSettingsCommandFactory>()
                .As<IPluginSettingsCommandFactory>();

            containerBuilder.RegisterType<PluginSettingsViewModel>()
                .As<PluginSettingsViewModel>()
                .As<IDisplayAvailability>()
                .SingleInstance();

            containerBuilder.RegisterType<SimhubPluginConfigurationRepository>()
                .As<IPitWallConfiguration>()
                .SingleInstance();

            containerBuilder.RegisterType<MappingConfigurationRepository>()
                .As<IMappingConfigurationRepository>();

            containerBuilder.RegisterType<SettingsValidatorWrapper>()
                .As<ISettingsValidator>();

            containerBuilder.RegisterType<ForwarderServiceFactory>()
                .As<IForwarderServiceFactory>();

            containerBuilder.RegisterType<FakeLeaderboardLiveAggregator>()
                .As<ILeaderboardLiveAggregator>();

            containerBuilder.RegisterType<AcquisitionService>()
                .As<IAcquisitionService>();

            var builder = containerBuilder.Build();

            return builder;
        }

        // ===========================================================

        private PluginManager _pluginManager;

        public PluginManager PluginManager { set => _pluginManager = value; }

        public void DataUpdate(PluginManager pluginManager, ref GameData data)
        {
            // THOUGHT: add call to service here to grab the data we want from plugin manager
            IPluginRecordRepository pluginRecordRepository
                = _pluginRecordFactory.GetInstance(pluginManager);

            /**
             * Idea: use ioc framework to host plugin manager
             * */

            _acquisitionService.HandleDataUpdate(pluginRecordRepository);

            //_webApiForwarderService.HandleDataUpdate(pluginRecordRepository);
        }

        public void End(PluginManager pluginManager)
        {
            _logger.Info("Data plugin closing ...");

            _acquisitionService.Stop();

            _logger.Info("Data plugin closing DONE!");
        }

        public void Init(PluginManager pluginManager)
        {
            _logger.Info("Starting Fam Data Gathering plugin ...");

            /**
             * Idea: use ioc framework to split classes when too big.
             * */

            _acquisitionService.Start();

            _logger.Info("Starting Fam Data Gathering plugin DONE!");
        }

        public Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new PluginSettings(_builder.Resolve<PluginSettingsViewModel>());
        }

        // ===========================================================
    }
}
