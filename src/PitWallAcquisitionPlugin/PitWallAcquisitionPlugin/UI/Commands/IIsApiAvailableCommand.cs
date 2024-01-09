using System.Windows.Input;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    internal interface IIsApiAvailableCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}