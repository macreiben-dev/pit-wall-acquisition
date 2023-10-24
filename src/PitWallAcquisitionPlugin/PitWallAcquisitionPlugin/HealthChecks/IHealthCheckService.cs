using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks
{
    public interface IHealthCheckService
    {
        Task<bool> Check(string originalApiAddress);
    }
}