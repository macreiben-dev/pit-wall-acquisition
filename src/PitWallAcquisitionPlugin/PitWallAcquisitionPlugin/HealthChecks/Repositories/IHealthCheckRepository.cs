using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    public interface IHealthCheckRepository
    {
        Task<bool> Check(string apiAddress);
    }
}
