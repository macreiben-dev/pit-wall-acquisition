using System.Net.Http;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    public interface IPitwallRemoteClient
    {
        void PostAsync(string uriPath, HttpContent payload);
    }
}
