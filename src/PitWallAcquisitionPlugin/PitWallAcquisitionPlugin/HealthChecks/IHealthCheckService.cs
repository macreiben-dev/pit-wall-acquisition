using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks
{
    internal interface IHealthCheckService
    {
        Task<bool> Check(string originalApiAddress);
    }
}