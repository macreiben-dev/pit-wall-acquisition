﻿using System;
using System.ComponentModel;

namespace PitWallAcquisitionPlugin.UI.Commands
{
    internal interface ILocalWorkerFactory
    {
        ILocalWorker GetInstance(
            Action<ILocalWorker, DoWorkEventArgs> doWork, 
            Action<ILocalWorker, ProgressChangedEventArgs> progressChanged, 
            Action<ILocalWorker, RunWorkerCompletedEventArgs> completed);
    }
}