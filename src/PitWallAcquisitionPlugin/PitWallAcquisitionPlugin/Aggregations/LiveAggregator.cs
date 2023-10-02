using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;
using System;

namespace PitWallAcquisitionPlugin.Aggregations
{
    public sealed class LiveAggregator : ILiveAggregator
    {
        private string _sessionTimeLeft = string.Empty;
        private bool _dirty = false;
        private string _pilotName;
        private double _laptimeMilliseconds;

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
            throw new NotImplementedException("Should add add laptime here.");
        }
        public void Clear()
        {
            SetClean();
            _sessionTimeLeft = string.Empty.ToString();
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

        private void SetDirty()
        {
            _dirty = true;
        }

        private void SetClean()
        {
            _dirty = true;
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
    }
}
