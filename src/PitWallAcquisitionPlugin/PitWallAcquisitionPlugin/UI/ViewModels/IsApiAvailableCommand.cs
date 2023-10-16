using PitWallAcquisitionPlugin.Repositories;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{

    public sealed class IsApiAvailableCommand : IIsApiAvailableCommand
    {
        private readonly IDisplayAvailability viewModel;
        private readonly IPitWallApiStatusRepository statusRepo;

        public IsApiAvailableCommand(
            IDisplayAvailability viewModel,
            IPitWallApiStatusRepository statusRepo)
        {
            this.viewModel = viewModel;
            this.statusRepo = statusRepo;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            return SettingsValidators.IsUriValid(parameter.ToString());
        }

        public void Execute(object parameter)
        {
            var isAvailable = statusRepo.IsAvailable(parameter.ToString());

            viewModel.IsApiAvailable = isAvailable;
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