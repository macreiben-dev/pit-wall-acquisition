﻿using System.Net.Http;
using System.Net;
using System.Text;
using System;
using System.Threading.Tasks;
using FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Repositories
{
    public sealed class PitWallTelemetryRemoteRepository : IStagingTelemetryDataRepository
    {
        private readonly IPitwallRemoteRepository _remoteRepository;

        public string Uri { get; }

        public PitWallTelemetryRemoteRepository(IPitWallConfiguration configuration, string relativeUri)
        {
            /**
             * Not very happy with this one. Could have a repository of remotes organized by uri... 
             * */
            _remoteRepository = new PitwallRemoteRepository(configuration, relativeUri);

            Uri = relativeUri;
        }

        public Task SendAsync(object dataToSend)
        {
            return _remoteRepository.SendAsync(dataToSend);
        }
    }

    public sealed class PitWallTelemetryRemoteRepositoryLegacy : IStagingTelemetryDataRepository
    {
        /**
         * Idea: use configuration from simhub to define this one.
         * 
         * */
        private const string RelativeUri = "/api/v1/Telemetry";

        private readonly HttpClient _httpClient;
        private readonly IPitWallConfiguration _configuration;

        public string Uri => RelativeUri;

        public PitWallTelemetryRemoteRepositoryLegacy(IPitWallConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

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
            return new Uri(new Uri(_configuration.ApiAddress), RelativeUri);
        }
    }
}
