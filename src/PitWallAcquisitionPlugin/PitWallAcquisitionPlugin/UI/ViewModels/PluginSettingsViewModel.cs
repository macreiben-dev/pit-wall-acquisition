using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Tests.UI.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsViewModel :
        INotifyPropertyChanged,
        IDataErrorInfo,
        IDisplayAvailability
    {
        private const string PILOTNAME_MUST_BE_SET = "Pilot name must be set.";
        private const string VALIDATION_APIADDRESS_MUST_BE_SET = "API address must be set.";
        private const string VALIDATION_APIADDRESS_URI_FORMAT = "API URI format is invalid. Should look like http://domain.ext or http://domain.ext";
        private const string VALIDATION_PERSONALKEY_LENGTH_INVALID = "Personal key length should be at least 10 character long.";
        private const string VALIDATION_PERSONALKEY_FORMAT_INVALID = "Personal should be made of alphanumerical character and \"-\", \"_\", \"@\".";

        private readonly IPitWallConfiguration _configuration;
        private readonly ILiveAggregator _aggregator;
        private readonly IPluginSettingsCommandFactory _cmdFactory;

        private string _isApiAvailable;

        public PluginSettingsViewModel(
            IPitWallConfiguration configuration, 
            ILiveAggregator aggregator, 
            IPluginSettingsCommandFactory cmdFactory)
        {
            _configuration = configuration;
            _aggregator = aggregator;
            _cmdFactory = cmdFactory;

            IsApiAvailableCommand = _cmdFactory.GetInstance(this);
        }

        public IIsApiAvailableCommand IsApiAvailableCommand { get; }


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
            get => _configuration.PilotName;
            set
            {
                _configuration.PilotName = value;
                NotifyPropertyChanged(nameof(PilotName));


                if (string.IsNullOrEmpty(this[nameof(PilotName)]))
                {
                    _aggregator.AddPilotName(value);
                }
            }
        }

        public string ApiAddress
        {
            get => _configuration.ApiAddress;
            set
            {
                _configuration.ApiAddress = value;

                NotifyPropertyChanged(nameof(ApiAddress));

                RaiseCommandChanged(); // UNTESTED
            }
        }

        private void RaiseCommandChanged()
        {
            IsApiAvailableCommand.RaiseCanExecuteChanged();
        }

        public string PersonalKey
        {
            get => _configuration.PersonalKey;
            set
            {
                _configuration.PersonalKey = value;
                NotifyPropertyChanged(nameof(PersonalKey));

                /**
                 * Idea: This should be replaced by a configuration repository
                 * that is used accross the application. A view model is too
                 * specialised to change liveaggregator state directly.
                 * 
                 * */
                if (string.IsNullOrEmpty(this[nameof(PersonalKey)]))
                {
                    _aggregator.AddSimerKey(value);
                }

                RaiseCommandChanged(); // UNTESTED
            }
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
