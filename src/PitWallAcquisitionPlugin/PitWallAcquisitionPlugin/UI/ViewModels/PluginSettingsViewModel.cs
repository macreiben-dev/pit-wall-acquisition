using PitWallAcquisitionPlugin.Tests.UI.Commands;
using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsViewModel :
        INotifyPropertyChanged,
        IDataErrorInfo,
        IDisplayAvailability,
        IUserDefinedConfiguration
    {
        private const string CARNAME_MUST_BE_SET = "Car name must be set.";

        private readonly IPluginSettingsCommandFactory _cmdFactory;
        private readonly ISettingsValidator _settingsValidator;
        private string _isApiAvailable;

        private string _carName;

        private string _pilotName;

        private string _apiAddress;

        private string _personalKey;

        public PluginSettingsViewModel(
            IPitWallConfiguration configuration,
            IPluginSettingsCommandFactory cmdFactory, 
            ISettingsValidator settingsValidator)
        {
            _cmdFactory = cmdFactory;
            _settingsValidator = settingsValidator;
            IsApiAvailableCommand = _cmdFactory.GetInstance(this);

            SaveToConfigurationCommand = _cmdFactory.GetSaveToConfiguration();

            _pilotName = configuration.PilotName;
            _carName = configuration.CarName;
            _apiAddress = configuration.ApiAddress;
            _personalKey = configuration.PersonalKey;
        }

        public IIsApiAvailableCommand IsApiAvailableCommand { get; }
        public ISaveToConfigurationCommand SaveToConfigurationCommand { get; }

        public string IsApiAvailable
        {
            get => _isApiAvailable;
            set
            {
                _isApiAvailable = value;
                NotifyPropertyChanged(nameof(IsApiAvailable));
            }
        }

        public string PilotName
        {
            get => _pilotName;
            set
            {
                _pilotName = value;
                NotifyPropertyChanged(nameof(PilotName));
                RaiseCommandChanged();
            }
        }

        public string CarName
        {
            get => _carName;
            set
            {
                _carName = value;
                NotifyPropertyChanged(nameof(CarName));
                RaiseCommandChanged();
            }

        }

        public string ApiAddress
        {
            get => _apiAddress;
            set
            {
                _apiAddress = value;
                NotifyPropertyChanged(nameof(ApiAddress));
                RaiseCommandChanged();
            }
        }

        public string PersonalKey
        {
            get => _personalKey;
            set
            {
                _personalKey = value;
                NotifyPropertyChanged(nameof(PersonalKey));

                RaiseCommandChanged();
            }
        }

        public void RaiseCommandChanged()
        {
            IsApiAvailableCommand.RaiseCanExecuteChanged();
            SaveToConfigurationCommand.RaiseCanExecuteChanged();
        }

        public void SyncGUI()
        {
            var propNames = new[] { 
                nameof(ApiAddress),
                nameof(PilotName), 
                nameof(PersonalKey),
                nameof(CarName)};

            foreach (var prop in propNames)
            {
                NotifyPropertyChanged(prop);

            }

            RaiseCommandChanged();
        }

        #region Observability

        public string this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case nameof(PilotName):
                        return DataValidator.PilotName.IsValid(PilotName);
                    case nameof(CarName):
                        return DataValidator.CarName.IsValid(CarName); 
                    case nameof(ApiAddress):
                        return _settingsValidator.IsApiAddressValid(ApiAddress);
                    case nameof(PersonalKey):
                        return _settingsValidator.IsPersonalKeyValid(PersonalKey);
                }

                return null;
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            SimHub.Logging.Current.Debug($"Property changed [{propertyName}]");

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Observability
    }
}
