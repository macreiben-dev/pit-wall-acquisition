﻿using Autofac;
using FuelAssistantMobile.DataGathering.SimhubPlugin;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using GameReaderCommon;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using PitWallAcquisitionPlugin.Repositories;
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
        private readonly ILogger _logger;
        private readonly IWebApiForwarderService _webApiForwarderService;

        public WebApiForwarder()
            : this(
                  new SimhubLogger(),
                  new LiveAggregator(),
                  new PitWallRemoteRepository(),
                  new PluginRecordRepositoryFactory())
        {

        }

        public WebApiForwarder(
            ILogger logger,
            ILiveAggregator aggregator,
            IStagingDataRepository dataRepository,
            IPluginRecordRepositoryFactory pluginRecordFactory)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(logger);
            containerBuilder.RegisterInstance(aggregator).SingleInstance();
            containerBuilder.RegisterInstance(dataRepository).SingleInstance();
            containerBuilder.RegisterInstance(pluginRecordFactory).SingleInstance();
            containerBuilder.RegisterType<WebApiForwarderService>()
                .As<IWebApiForwarderService>()
                .WithParameter("postToApiTimerHz", 10)
                .WithParameter("autoReactivateTimer", 5000)
                .SingleInstance();

            var builder = containerBuilder.Build(); 

            _logger = builder.Resolve<ILogger>();

            _pluginRecordFactory = builder.Resolve<IPluginRecordRepositoryFactory>();
            
            _webApiForwarderService = builder.Resolve<IWebApiForwarderService>();
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

            /**
             * Idea: use ioc framework to split classes when too big.
             * */

            _webApiForwarderService.Start();

            _logger.Info("Starting Fam Data Gathering plugin DONE!");
        }

        public Control GetWPFSettingsControl(PluginManager pluginManager)
        {
            return new PluginSettings();
        }

        // ===========================================================
    }
}
