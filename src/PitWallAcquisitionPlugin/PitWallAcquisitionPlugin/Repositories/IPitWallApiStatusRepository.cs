namespace PitWallAcquisitionPlugin.Repositories
{
    internal interface IPitWallApiStatusRepository
    {
        bool IsAvailable(string apiUri);
    }
}