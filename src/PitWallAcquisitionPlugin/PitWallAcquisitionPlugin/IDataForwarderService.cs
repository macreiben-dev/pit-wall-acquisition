namespace PitWallAcquisitionPlugin
{
    internal interface IDataForwarderService
    {
        void HandleDataUpdate(IPluginRecordRepository pluginRecordRepository);
        void Start();
        void Stop();
    }
}