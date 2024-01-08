using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin
{
    public interface IDataForwarderService
    {
        void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository);
        void Start();
        void Stop();
    }
}