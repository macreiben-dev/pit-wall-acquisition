using PitWallAcquisitionPlugin.Aggregations.Aggregators;
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
        private const string PILOTNAME_MUST_BE_SET = "Pilot name must be set.";
        private const string CARNAME_MUST_BE_SET = "Car name must be set.";

        private const string VALIDATION_APIADDRESS_MUST_BE_SET = "API address must be set.";
        private const string VALIDATION_APIADDRESS_URI_FORMAT = "API URI format is invalid. Should look like http://domain.ext or http://domain.ext";
        private const string VALIDATION_PERSONALKEY_LENGTH_INVALID = "Personal key length should be at least 10 character long.";
        private const string VALIDATION_PERSONALKEY_FORMAT_INVALID = "Personal should be made of alphanumerical character and \"-\", \"_\", \"@\".";

        private readonly IPluginSettingsCommandFactory _cmdFactory;

        private string _isApiAvailable;

        private string _carName;

        private string _pilotName;

        private string _apiAddress;

        private string _personalKey;

        public PluginSettingsViewModel(
            IPitWallConfiguration configuration,
            IPluginSettingsCommandFactory cmdFactory)
        {
            _cmdFactory = cmdFactory;

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
                        if (string.IsNullOrEmpty(PilotName) ||
                            string.IsNullOrWhiteSpace(PilotName))
                        {
                            return PILOTNAME_MUST_BE_SET;
                        }
                        break;
                    case nameof(CarName):
                        if (string.IsNullOrEmpty(CarName) ||
                            string.IsNullOrWhiteSpace(CarName))
                        {
                            return CARNAME_MUST_BE_SET;
                        }
                        break;
                    case nameof(ApiAddress):
                        if (string.IsNullOrEmpty(ApiAddress) ||
                            string.IsNullOrWhiteSpace(ApiAddress))
                        {
                            return VALIDATION_APIADDRESS_MUST_BE_SET;
                        }
                        bool isFormatValid = SettingsValidators.IsUriValid(ApiAddress);

                        if (!isFormatValid)
                        {
                            return VALIDATION_APIADDRESS_URI_FORMAT;
                        }
                        break;
                    case nameof(PersonalKey):
                        if (string.IsNullOrEmpty(PersonalKey) ||
                           string.IsNullOrWhiteSpace(PersonalKey))
                        {
                            return VALIDATION_PERSONALKEY_FORMAT_INVALID;
                        }

                        if (PersonalKey.Length < 10)
                        {
                            return VALIDATION_PERSONALKEY_LENGTH_INVALID;
                        }

                        break;
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
