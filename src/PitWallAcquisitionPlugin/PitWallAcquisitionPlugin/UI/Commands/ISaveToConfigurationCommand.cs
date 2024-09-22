using System.Windows.Input;

namespace PitWallAcquisitionPlugin.UI.Commands
{
    internal interface ISaveToConfigurationCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
