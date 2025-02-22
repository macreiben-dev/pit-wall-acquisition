﻿using System;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.UI.Commands
{
    internal sealed class SaveToConfigurationCommand : ISaveToConfigurationCommand
    {
        private IPitWallConfiguration _configuration;
        private ISettingsValidator _validator;

        public event EventHandler CanExecuteChanged;

        public SaveToConfigurationCommand(IPitWallConfiguration configuration, ISettingsValidator validator)
        {
            _configuration = configuration;
            _validator = validator;
        }

        public bool CanExecute(object parameter)
        {
            var config = parameter as IUserDefinedConfiguration;

            if(config == null)
            {
                return false;
            }

            return _validator.IsPilotNameValid(config.PilotName) 
                && _validator.IsPersonalKeyValid(config.PersonalKey)
                && _validator.IsCarNameValid(config.CarName)
                && _validator.IsApiAddressValid(config.ApiAddress);
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
