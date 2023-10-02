using System.Net.Http;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories
{
    public sealed class FamRemoteRepository : IStagingDataRepository
    {
        private const string WebApiUrl = "https://localhost:32786/Inbound";
        private HttpClient _httpClient;

        public FamRemoteRepository()
        {
            _httpClient = new HttpClient();
        }

        public async Task SendAsync(object dataToSend)
        {
            // Convert the data to JSON

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

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
