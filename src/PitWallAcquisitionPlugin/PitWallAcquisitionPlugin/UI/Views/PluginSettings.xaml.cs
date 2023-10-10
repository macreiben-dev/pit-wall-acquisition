using PitWallAcquisitionPlugin.UI.ViewModels;
using System.Windows.Controls;

namespace PitWallAcquisitionPlugin.UI.Views
{
    /// <summary>
    /// Interaction logic for PluginSettings.xaml
    /// </summary>
    public partial class PluginSettings : UserControl
    {
        public PluginSettings()
        {
            InitializeComponent();

            this.Loaded += PluginSettings_Loaded;

           
        }

        private void PluginSettings_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            /**
             * Idea : load configuration from saved configuration.
             * Should create inject an implementation of configuration repository to do the heavy lifting.
             * */

            /**
             * Idea: consider using an ioc engine to create what needs to be created. 
             * Eventualy pass reference on plugin to add a Start/Stop button also.
             * */
            DataContext = new PluginSettingsViewModel();
        }
    }
}
