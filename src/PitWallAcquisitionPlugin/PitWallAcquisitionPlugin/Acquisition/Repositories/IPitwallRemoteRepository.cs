using System.Threading.Tasks;

namespace PitWallAcquisitionPlugin.Acquisition.Repositories
{
    internal interface IPitwallRemoteRepository
    {
        Task SendAsync(object dataToSend);

        string Uri { get; }
    }
}