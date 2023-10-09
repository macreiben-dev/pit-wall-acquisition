using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private const string PILOTNAME_MUST_BE_SET = "Pilot name must be set.";
        private const string VALIDATION_APIADDRESS_MUST_BE_SET = "API address must be set.";
        private const string VALIDATION_APIADDRESS_URI_FORMAT = "API URI format is invalid. Should look like http://domain.ext or http://domain.ext";

        private string _pilotName;
        private string _apiAddress;

        public string PilotName
        {
            get => _pilotName;
            set
            {
                _pilotName = value;
                NotifyPropertyChanged(nameof(PilotName));
            }
        }

        public string ApiAddress
        {
            get => _apiAddress;
            set
            {
                _apiAddress = value;
                NotifyPropertyChanged(nameof(ApiAddress));
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

                        Uri convetedUri = null;

                        var isFormatValid = Uri.TryCreate(
                            ApiAddress, 
                            UriKind.Absolute,        
                            out convetedUri)
                            && convetedUri != null && (
                                convetedUri.Scheme == Uri.UriSchemeHttp 
                                || convetedUri.Scheme == Uri.UriSchemeHttps);

                        if (!isFormatValid)
                        {
                            return VALIDATION_APIADDRESS_URI_FORMAT;
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
            SimHub.Logging.Current.Info($"Property changed [{propertyName}]");

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Observability
    }
}
