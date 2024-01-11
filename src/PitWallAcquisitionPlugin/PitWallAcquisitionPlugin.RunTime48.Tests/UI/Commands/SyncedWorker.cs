using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    internal class SyncedWorker : ILocalWorker
    {
        private Action<ILocalWorker, DoWorkEventArgs> doWork;
        private Action<ILocalWorker, ProgressChangedEventArgs> progressChanged;
        private Action<ILocalWorker, RunWorkerCompletedEventArgs> completed;

        public SyncedWorker(Action<ILocalWorker, DoWorkEventArgs> doWork, Action<ILocalWorker, ProgressChangedEventArgs> progressChanged, Action<ILocalWorker, RunWorkerCompletedEventArgs> completed)
        {
            this.doWork = doWork;
            this.progressChanged = progressChanged;
            this.completed = completed;
        }

        public void ReportProgress(int percent)
        {
            progressChanged(this, new ProgressChangedEventArgs(percent, this));
        }

        public void Run()
        {
            DoWorkEventArgs doWorkArg = new DoWorkEventArgs(this);

            doWork(this, doWorkArg);

            completed(this, new RunWorkerCompletedEventArgs(doWorkArg.Result, null, false));
        }

    }
}
