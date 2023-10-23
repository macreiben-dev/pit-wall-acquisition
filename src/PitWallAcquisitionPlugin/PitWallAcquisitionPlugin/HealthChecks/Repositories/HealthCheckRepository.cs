using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    public class HealthCheckRepository : IHealthCheckRepository
    {
        private const string RequestUri = "/api/healthcheck";

        public async Task<bool> Check(string apiAddress)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(apiAddress);

            try { 

            var response = await client.GetAsync(
                RequestUri);

                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }
    }
}
