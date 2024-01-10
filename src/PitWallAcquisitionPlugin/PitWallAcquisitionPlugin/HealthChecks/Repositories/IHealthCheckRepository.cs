using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    internal interface IHealthCheckRepository
    {
        Task<bool> Check(string apiAddress);
    }
}
