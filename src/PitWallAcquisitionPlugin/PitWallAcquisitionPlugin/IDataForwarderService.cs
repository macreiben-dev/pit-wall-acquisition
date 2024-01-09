using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin
{
    internal interface IDataForwarderService
    {
        void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository);
        void Start();
        void Stop();
    }
}