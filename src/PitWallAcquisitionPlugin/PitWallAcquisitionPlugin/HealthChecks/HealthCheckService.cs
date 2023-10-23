﻿using PitWallAcquisitionPlugin.HealthChecks.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks
{
    public sealed class HealthCheckService
    {
        private readonly IHealthCheckRepository repo;

        public HealthCheckService(IHealthCheckRepository repo)
        {
            this.repo = repo;
        }

        public async Task<bool> Check(string originalApiAddress)
        {
            if (!SettingsValidators.IsUriValid(originalApiAddress))
            {
                return false;
            }

            return await repo.Check(originalApiAddress);
        }
    }
}