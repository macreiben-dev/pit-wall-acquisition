namespace PitWallAcquisitionPlugin.Repositories
{
    public interface IPitWallApiStatusRepository
    {
        bool IsAvailable(string apiUri);
    }
}