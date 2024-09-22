using Autofac;
using GameReaderCommon;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using PitWallAcquisitionPlugin.UI.ViewModels;
using PitWallAcquisitionPlugin.UI.Views;
using SimHub.Plugins;
using System.Windows.Controls;
using log4net;
using PitWallAcquisitionPlugin.Logging;

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
        private readonly IAcquisitionService _acquisitionService;

        public WebApiForwarder()
            : this(SimHub.Logging.Current)
        {
        }

        internal WebApiForwarder(ILog current)
        {
            IContainer builder = IocContainerInitialization.CreateBuilder(
                current,
                this);

            _builder = builder;

            _logger = _builder.Resolve<ILogger>();

            _pluginRecordFactory = _builder.Resolve<IPluginRecordRepositoryFactory>();

            _acquisitionService = _builder.Resolve<IAcquisitionService>();
        }

        // ===========================================================

        private PluginManager _pluginManager;

        public PluginManager PluginManager
        {
            set => _pluginManager = value;
        }

        public void DataUpdate(PluginManager pluginManager,
            ref GameData data)
        {
            if (!data.GameRunning)
            {
                return;
            }

            IPluginRecordRepository pluginRecordRepository
                = _pluginRecordFactory.GetInstance(pluginManager);

            _acquisitionService.HandleDataUpdate(pluginRecordRepository);
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