using FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations;
using System;
using System.Globalization;

namespace PitWallAcquisitionPlugin.Aggregations
{
    public sealed class LiveAggregator : ILiveAggregator
    {
        private string _sessionTimeLeft = string.Empty;
        private bool _dirty = false;
        private string _pilotName = "MacReibenFromPlugin";

        private double? _laptimeSeconds;
        private double? _frontLeftTyreWear;
        private double? _frontRightTyreWear;
        private double? _rearLeftTyreWear;
        private double? _rearRightTyreWear;

        private double? _frontLeftTyreTemp;
        private double? _frontRightTyreTemp;
        private double? _rearLeftTyreTemp;
        private double? _rearRightTyreTemp;

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

            _laptimeSeconds = duration.TotalMilliseconds / 1000;

            SetDirty();
        }

        public void Clear()
        {
            SetClean();

            _sessionTimeLeft = null;
            _laptimeSeconds = null;

            _frontLeftTyreWear = null;
            _frontRightTyreWear = null;

            _rearLeftTyreWear = null;
            _rearRightTyreWear = null;
        }

        public IData AsData()
        {
            return new Data
            {
                SessionTimeLeft = _sessionTimeLeft,
                PilotName = _pilotName,
                LaptimeSeconds = _laptimeSeconds,
                TyresWear = new Tyres()
                {
                    FrontLeftWear = _frontLeftTyreWear,
                    FrontRightWear = _frontRightTyreWear,
                    ReartLeftWear = _rearLeftTyreWear,
                    RearRightWear = _rearRightTyreWear,
                },
                TyresTemperatures = new TyresTemperatures()
                {
                    FrontLeftTemp = _frontLeftTyreTemp,
                    FrontRightTemp = _frontRightTyreTemp,
                    RearLeftTemp = _rearLeftTyreTemp,
                    RearRightTemp = _rearRightTyreTemp
                }
            };
        }

        #region tyre wear

        public void AddFrontLeftTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _frontLeftTyreWear = tyreWearValue;

            SetDirty();
        }

        public void AddFrontRightTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _frontRightTyreWear = tyreWearValue;

            SetDirty();
        }

        public void AddRearLeftTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _rearLeftTyreWear = tyreWearValue;

            SetDirty();
        }

        public void AddRearRightTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _rearRightTyreWear = tyreWearValue;

            SetDirty();
        }

        #endregion tyre wear

        #region tyre temperature

        public void AddFrontLeftTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _frontLeftTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void AddFrontRightTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _frontRightTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void AddRearLeftTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _rearLeftTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void AddRearRightTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _rearRightTyreTemp = tyreTempValue;

            SetDirty();
        }

        #endregion tyre temp

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
