namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    public interface ILocalWorker
    {
        void Run();
        void ReportProgress(int percent);
    }
}