using Autofac;
using log4net;
using PitWallAcquisitionPlugin.Acquisition;
using PitWallAcquisitionPlugin.Acquisition.Repositories;
using PitWallAcquisitionPlugin.Aggregations.Leadeboards;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.HealthChecks.Repositories;
using PitWallAcquisitionPlugin.Logging;
using PitWallAcquisitionPlugin.PluginManagerWrappers;
using PitWallAcquisitionPlugin.Repositories;
using PitWallAcquisitionPlugin.Tests.UI.Commands;
using PitWallAcquisitionPlugin.UI.ViewModels;
using SimHub.Plugins;

namespace PitWallAcquisitionPlugin
{
    internal static class IocContainerInitialization
    {

        public static IContainer CreateBuilder(
            ILog log,
            IDataPlugin plugin)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterInstance(plugin).As<IPlugin>();

            containerBuilder.RegisterType<TelemetryLiveAggregator>()
                .As<ITelemetryLiveAggregator>()
                .SingleInstance();

            //-------------

            containerBuilder.RegisterInstance(log)
                .As<ILog>()
                .SingleInstance();

            containerBuilder.RegisterType<SimhubLogger>()
                .As<ILogger>()
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

            containerBuilder.RegisterType<PluginSettingsCommandFactory>()
                .As<IPluginSettingsCommandFactory>();

            containerBuilder.RegisterType<PluginSettingsViewModel>()
                .As<PluginSettingsViewModel>()
                .As<IDisplayAvailability>()
                .SingleInstance();

            containerBuilder.RegisterType<LocalWorkerFactory>()
                .As<ILocalWorkerFactory>();

            containerBuilder.RegisterType<SimhubPluginConfigurationRepository>()
                .As<IPitWallConfiguration>()
                .SingleInstance();

            containerBuilder.RegisterType<MappingConfigurationRepository>()
                .As<IMappingConfigurationRepository>();

            containerBuilder.RegisterType<SettingsValidatorWrapper>()
                .As<ISettingsValidator>();

            containerBuilder.RegisterType<ForwarderServiceFactory>()
                .As<IForwarderServiceFactory>();

            containerBuilder.RegisterType<LeaderboardLiveAggregator>()
                .As<ILeaderboardLiveAggregator>();

            containerBuilder.RegisterType<AcquisitionService>()
                .As<IAcquisitionService>();

            var builder = containerBuilder.Build();

            return builder;
        }
    }
}