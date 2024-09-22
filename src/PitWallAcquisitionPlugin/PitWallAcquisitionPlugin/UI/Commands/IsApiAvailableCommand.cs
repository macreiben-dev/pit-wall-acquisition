using System;
using System.ComponentModel;
using System.Diagnostics;
using PitWallAcquisitionPlugin.HealthChecks;
using PitWallAcquisitionPlugin.UI.ViewModels;

namespace PitWallAcquisitionPlugin.UI.Commands
{

    internal sealed class IsApiAvailableCommand : IIsApiAvailableCommand
    {
        private readonly IDisplayAvailability viewModel;
        private readonly IHealthCheckService statusRepo;
        private readonly ILocalWorkerFactory workerFactory;

        public IsApiAvailableCommand(
            IDisplayAvailability viewModel,
            IHealthCheckService statusRepo,
            ILocalWorkerFactory workerFactory)
        {
            this.viewModel = viewModel;
            this.statusRepo = statusRepo;
            this.workerFactory = workerFactory;
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

            Action<ILocalWorker, DoWorkEventArgs> doWork = (o, e) => {
                Trace.WriteLine(nameof(IsApiAvailableCommand) + " - Worker started.");

                var isAvailable = statusRepo.Check(parameter.ToString());

                o.ReportProgress(50);

                var status = isAvailable.GetAwaiter().GetResult();

                e.Result = status;
            };

            Action<ILocalWorker, ProgressChangedEventArgs> progressChanged = (o, e) =>
            {
                viewModel.IsApiAvailable = $"{e.ProgressPercentage} %";
            };

            Action<ILocalWorker, RunWorkerCompletedEventArgs> completed = (o, e) =>
            {
                bool actual = (bool)e.Result;

                viewModel.IsApiAvailable = actual ? "OK" : "KO";

                Trace.WriteLine(nameof(IsApiAvailableCommand) + " - Worker completed.");
            };

            var worker = workerFactory.GetInstance(
                doWork,
                progressChanged,
                completed);

            worker.Run();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }

    internal class LocalWorkerFactory : ILocalWorkerFactory
    {
        public ILocalWorker GetInstance(
            Action<ILocalWorker, DoWorkEventArgs> doWork,
            Action<ILocalWorker, ProgressChangedEventArgs> progressChanged,
            Action<ILocalWorker, RunWorkerCompletedEventArgs> completed)
        {
            return new LocalWorker(doWork,
                progressChanged,
                completed);
        }
    }

    internal class LocalWorker : ILocalWorker
    {
        private readonly Action<ILocalWorker, DoWorkEventArgs> doWork;
        private readonly Action<ILocalWorker, ProgressChangedEventArgs> progressChanged;
        private readonly Action<ILocalWorker, RunWorkerCompletedEventArgs> completed;
        private BackgroundWorker worker;

        public LocalWorker(
            Action<ILocalWorker, DoWorkEventArgs> doWork,
            Action<ILocalWorker, ProgressChangedEventArgs> progressChanged,
            Action<ILocalWorker, RunWorkerCompletedEventArgs> completed)
        {
            this.doWork = doWork;
            this.progressChanged = progressChanged;
            this.completed = completed;
            worker = new BackgroundWorker();

        }

        public void Run()
        {

            worker.WorkerReportsProgress = true;

            worker.DoWork += (o, e) =>
            {
                doWork(this, e);
            };

            worker.ProgressChanged += (o, e) =>
            {
                progressChanged(this, e);
            };

            worker.RunWorkerCompleted += (o, e) =>
            {
                completed(this, e);
            };

            worker.RunWorkerAsync();
        }

        public void ReportProgress(int percent)
        {
            worker.ReportProgress(percent);
        }
    }
}