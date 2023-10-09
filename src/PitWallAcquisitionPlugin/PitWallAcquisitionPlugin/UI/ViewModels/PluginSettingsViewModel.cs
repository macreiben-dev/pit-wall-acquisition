using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private const string PILOTNAME_MUST_BE_SET = "Pilot name must be set.";
        private string _pilotName;

        public string PilotName
        {
            get => _pilotName;
            set
            {
                _pilotName = value;
                NotifyPropertyChanged(nameof(PilotName));
            }
        }

        #region Observability

        public string this[string propertyName]
        {
            get
            {
                SimHub.Logging.Current.Info($"DataErrorInfo called [{propertyName}]");

                switch (propertyName)
                {
                    case nameof(PilotName):
                        if (string.IsNullOrEmpty(PilotName) || 
                            string.IsNullOrWhiteSpace(PilotName))
                        {
                            return PILOTNAME_MUST_BE_SET;
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
