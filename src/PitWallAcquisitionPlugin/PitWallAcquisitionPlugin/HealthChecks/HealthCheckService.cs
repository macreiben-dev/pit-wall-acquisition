using PitWallAcquisitionPlugin.HealthChecks.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.HealthChecks
{
    public sealed class HealthCheckService
    {
        private readonly IHealthCheckRepository repo;

        public HealthCheckService(IHealthCheckRepository repo)
        {
            this.repo = repo;
        }

        public bool Check(string originalApiAddress)
        {
            if (!SettingsValidators.IsUriValid(originalApiAddress))
            {
                return false;
            }

            return repo.Check(originalApiAddress);
        }
    }
}