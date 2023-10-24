using System.Windows.Input;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    public interface IIsApiAvailableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}