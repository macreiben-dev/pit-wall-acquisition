using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        public async Task<bool> Check(string originalApiAddress)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(originalApiAddress);

            var response = await client.GetAsync("/healthcheck");

            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
