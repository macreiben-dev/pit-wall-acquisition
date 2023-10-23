namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    public interface IHealthCheckRepository
    {
        bool Check(string originalApiAddress);
    }
}
