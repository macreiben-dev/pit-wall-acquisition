using System.Threading.Tasks;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Repositories
{
    public interface IStagingDataRepository
    {
        Task SendAsync(object dataToSend);
    }
}