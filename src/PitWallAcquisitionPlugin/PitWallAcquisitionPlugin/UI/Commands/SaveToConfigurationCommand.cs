using System;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public sealed class SaveToConfigurationCommand : ISaveToConfigurationCommand
    {
        private IPitWallConfiguration _configuration;

        public event EventHandler CanExecuteChanged;

        public SaveToConfigurationCommand(IPitWallConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CanExecute(object parameter)
        {
            var config = parameter as IUserDefinedConfiguration;

            // TOPO : validate input here aswell

            return config != null;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
            {
                return;
            }

            var config = parameter as IUserDefinedConfiguration;

            _configuration.PilotName = config.PilotName;
            _configuration.PersonalKey = config.PersonalKey;
            _configuration.ApiAddress = config.ApiAddress;
            _configuration.CarName = config.CarName;
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
