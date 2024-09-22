namespace PitWallAcquisitionPlugin
{
    internal interface IAcquisitionService
    {
        void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository);
        void Start();
        void Stop();
    }
}