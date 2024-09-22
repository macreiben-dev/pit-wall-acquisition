using System;

namespace PitWallAcquisitionPlugin.Repositories
{
    public sealed class ErrorWhenSendDataException : Exception
    {
        public ErrorWhenSendDataException(
            string jsonData, 
            string webApiUrl, 
            Exception ex)
            : base($"Unable to contact remote API. [{webApiUrl}].", ex)
        {
            JsonData = jsonData;
            WebApiUrl = webApiUrl;
        }

        public string JsonData { get; }
        public string WebApiUrl { get; }
    }
}
