using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFluent;
using FluentAssertions;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.ViewModels
{
    public class PluginSettingsViewModelTest
    {
        [Fact]
        public void GIVEN_pilotName_isSet_THEN_notifyPropertychanged_pilotName() {

            var target = new PluginSettingsViewModel();

            using var monitored = target.Monitor();


            target.PilotName = "some_name";

            monitored.Should().Raise("PropertyChanged")
                .WithSender(target)
                .WithArgs<PropertyChangedEventArgs>(
                    e => e.PropertyName == "PilotName");
        }

    }
}
