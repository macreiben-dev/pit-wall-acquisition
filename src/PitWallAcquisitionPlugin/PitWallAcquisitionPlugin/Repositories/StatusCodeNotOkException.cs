﻿using System;
using System.Net;

namespace PitWallAcquisitionPlugin.Repositories
{
    public sealed class StatusCodeNotOkException : Exception
    {
        public StatusCodeNotOkException(HttpStatusCode code, string webApiUrl) 
            : base($"Status code received [{code}] - [{webApiUrl}]")
        {
            StatusCode = code;

            WebApiUrl = webApiUrl;
        }

        public HttpStatusCode StatusCode
        {
            get;
        }

        public string WebApiUrl
        {
            get;
        }
    }
}
