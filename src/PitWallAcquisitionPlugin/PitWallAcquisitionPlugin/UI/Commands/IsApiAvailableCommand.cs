using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{

    public sealed class IsApiAvailableCommand : IIsApiAvailableCommand
    {
        private readonly IDisplayAvailability viewModel;
        private readonly IHealthCheckService statusRepo;

        public IsApiAvailableCommand(
            IDisplayAvailability viewModel,
            IHealthCheckService statusRepo)
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
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (o, e) =>
            {
                var isAvailable = statusRepo.Check(parameter.ToString());

                worker.ReportProgress(50);

                var status = isAvailable.GetAwaiter().GetResult();

                e.Result = status;
            };

            worker.ProgressChanged += (o, e) =>
            {
                viewModel.IsApiAvailable = $"{e.ProgressPercentage} %";
            };

            worker.RunWorkerCompleted += (o, e) =>
            {
                bool actual = (bool) e.Result;

                viewModel.IsApiAvailable = actual ? "OK" : "KO";
            };

            worker.RunWorkerAsync();
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