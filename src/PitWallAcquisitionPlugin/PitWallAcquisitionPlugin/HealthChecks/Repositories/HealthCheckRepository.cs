using FuelAssistantMobile.DataGathering.SimhubPlugin.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.HealthChecks.Repositories
{
    internal class HealthCheckRepository : IHealthCheckRepository
    {
        private readonly object _lock = new object();

        private const string RequestUri = "/api/healthcheck";
        private ILogger _logger;
        private HttpClient _client;

        public HealthCheckRepository(ILogger logger)
        {
            _logger = logger;

        }

        public async Task<bool> Check(string apiAddress)
        {
            _logger.Info($"Checking connectivity for [{apiAddress}] ...");


            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(2);
            _client.BaseAddress = new Uri(apiAddress);

            try
            {
                var response = await _client.GetAsync(
                    RequestUri);

                return response.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _logger.Error("Exception occurect while checking status", ex);

                return false;
            }
            finally
            {
                _logger.Info($"Checking connectivity for [{apiAddress}] DONE!");
            }

        }
    }
}
