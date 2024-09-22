using System.Windows.Input;

namespace PitWallAcquisitionPlugin.UI.Commands
{
    internal interface IIsApiAvailableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}