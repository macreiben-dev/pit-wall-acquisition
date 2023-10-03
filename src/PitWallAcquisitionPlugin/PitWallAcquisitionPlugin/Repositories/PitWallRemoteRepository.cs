using System.Net.Http;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;

namespace PitWallAcquisitionPlugin.Repositories
{
    public sealed class PitWallRemoteRepository : IStagingDataRepository
    {
        /**
         * Idea: use configuration from simhub to define this one.
         * 
         * */
        private const string WebApiUrl = "http://localhost:32773/api/Telemetry";
        private readonly HttpClient _httpClient;

        public PitWallRemoteRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendAsync(object dataToSend)
        {
            // Convert the data to JSON

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
            StringContent content = new StringContent(
                jsonData, 
                Encoding.UTF8, 
                "application/json");

            try
            {
                // Post the data to the WebAPI
                var response = await _httpClient.PostAsync(WebApiUrl, content);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new StatusCodeNotOkException(response.StatusCode, WebApiUrl);
                }
            }
            catch (Exception ex)
            {
                throw new ErrorWhenSendDataException(jsonData, WebApiUrl, ex);
            }
        }
    }
}
