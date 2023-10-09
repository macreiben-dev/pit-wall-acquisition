using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.UI.ViewModels
{
    public class PluginSettingsViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
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

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Observability
    }
}
