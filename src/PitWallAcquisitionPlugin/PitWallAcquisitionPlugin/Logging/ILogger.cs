﻿using System;

namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Logging
{
    internal interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message);
        void Error(string message, Exception ex);
        void Warn(string message);
    }
}
