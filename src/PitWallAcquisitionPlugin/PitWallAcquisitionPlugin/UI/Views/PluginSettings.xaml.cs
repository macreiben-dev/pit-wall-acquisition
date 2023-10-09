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

            DataContext = new PluginSettingsViewModel();
        }
    }
}
