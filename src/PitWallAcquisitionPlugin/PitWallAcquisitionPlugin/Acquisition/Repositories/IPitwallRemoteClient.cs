using System.Net.Http;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    internal interface IPitwallRemoteClient
    {
        void PostAsync(string uriPath, HttpContent payload);
    }
}
