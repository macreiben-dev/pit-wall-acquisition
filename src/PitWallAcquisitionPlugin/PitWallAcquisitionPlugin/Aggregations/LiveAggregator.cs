﻿using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;
using System;
using System.Globalization;

namespace PitWallAcquisitionPlugin.Aggregations
{
    public sealed class LiveAggregator : ILiveAggregator
    {
        private string _sessionTimeLeft = string.Empty;
        private bool _dirty = false;
        private string _pilotName;
        private double? _laptimeMilliseconds;

        public bool IsDirty => _dirty;

        public void AddSessionTimeLeft(string sessionTimeLeft)
        {
            string trimmedSessionTimeLeft = sessionTimeLeft
                .ToString()
                .Substring(0, 8);

            if (_sessionTimeLeft != trimmedSessionTimeLeft)
            {
                _sessionTimeLeft = trimmedSessionTimeLeft;

                SetDirty();
            }
        }
        public void AddPilotName(string original)
        {
            if (original.ToString() != _pilotName)
            {
                _pilotName = original.ToString();

                SetDirty();
            }
        }

        public void AddLaptime(string original)
        {
            if (string.IsNullOrEmpty(original))
            {
                return;
            }

            var duration = TimeSpan.Parse(original, CultureInfo.InvariantCulture);

            _laptimeMilliseconds = duration.TotalMilliseconds;

            SetDirty();
        }

        public void Clear()
        {
            SetClean();

            _sessionTimeLeft = null;
            _laptimeMilliseconds = null;
        }

        public IData AsData()
        {
            return new Data
            {
                SessionTimeLeft = _sessionTimeLeft,
                PilotName = _pilotName,
                LaptimeMilliseconds = _laptimeMilliseconds
            };
        }

        public void AddFrontLeftTyreWear(double tyreWearValue)
        {
            throw new NotImplementedException();
        }

        public void AddFrontRightTyreWear(double tyreWearValue)
        {
            throw new NotImplementedException();
        }

        public void AddRearLeftTyreWear(double tyreWearValue)
        {
            throw new NotImplementedException();
        }

        public void AddRearRightTyreWear(double tyreWearValue)
        {
            throw new NotImplementedException();
        }
        private void SetDirty()
        {
            _dirty = true;
        }

        private void SetClean()
        {
            _dirty = false;
        }

    }
}
