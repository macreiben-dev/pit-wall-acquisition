using FuelAssistantMobile.DataGathering.SimhubPlugin;

namespace PitWallAcquisitionPlugin
{
    public interface IAcquisitionService
    {
        void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository);
        void Start();
        void Stop();
    }
}