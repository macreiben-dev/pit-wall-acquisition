using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.Tests.UI.Commands
{
    internal class FakeWorkerFactory : ILocalWorkerFactory
    {
        public ILocalWorker GetInstance(Action<ILocalWorker, DoWorkEventArgs> doWork, Action<ILocalWorker, ProgressChangedEventArgs> progressChanged, Action<ILocalWorker, RunWorkerCompletedEventArgs> completed)
        {
            return new SyncedWorker(doWork, progressChanged, completed);
        }
    }
}
