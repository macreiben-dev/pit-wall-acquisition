using System;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public sealed class SaveToConfigurationCommand : ISaveToConfigurationCommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var config = parameter as IUserDefinedConfiguration;

            return false;
        }

        public void Execute(object parameter)
        {
            var config = parameter as IUserDefinedConfiguration;


        }
    }
}
