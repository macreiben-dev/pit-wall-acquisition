using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PitWallAcquisitionPlugin.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    internal sealed class PitwallRemoteRepository : IPitwallRemoteRepository
    {
        /**
         * Idea: use configuration from simhub to define this one.
         * 
         * */
        private const string RelativeUri = "/api/v1/Telemetry";

        private readonly HttpClient _httpClient;
        private readonly IPitWallConfiguration _configuration;
        private readonly string _relativeUri;

        public PitwallRemoteRepository(IPitWallConfiguration configuration, string relativeUri)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _relativeUri = relativeUri;
        }

        public string Uri => _relativeUri;

        public async Task SendAsync(object dataToSend)
        {
            // Convert the data to JSON

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataToSend);
            StringContent content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            Uri apiUri = BuildApiUri();

            try
            {
                // Post the data to the WebAPI
                var response = await _httpClient.PostAsync(apiUri, content);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new StatusCodeNotOkException(response.StatusCode, apiUri.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new ErrorWhenSendDataException(jsonData, apiUri.ToString(), ex);
            }
        }

        private Uri BuildApiUri()
        {
            return new Uri(new Uri(_configuration.ApiAddress), _relativeUri);
        }
    }
}
