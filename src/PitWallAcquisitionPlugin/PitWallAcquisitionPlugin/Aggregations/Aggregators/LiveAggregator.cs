using PitWallAcquisitionPlugin.Aggregations.Aggregators.Models;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.Globalization;

namespace PitWallAcquisitionPlugin.Aggregations.Aggregators
{
    public sealed class LiveAggregator : ILiveAggregator
    {
        private string _sessionTimeLeft = string.Empty;
        private bool _dirty = false;

        private double? _laptimeSeconds;
        private double? _frontLeftTyreWear;
        private double? _frontRightTyreWear;
        private double? _rearLeftTyreWear;
        private double? _rearRightTyreWear;

        private double? _frontLeftTyreTemp;
        private double? _frontRightTyreTemp;
        private double? _rearLeftTyreTemp;
        private double? _rearRightTyreTemp;
        private double? _avgWetness;
        private double? _airTemperature;
        private readonly IPitWallConfiguration _configuration;

        public bool IsDirty => _dirty;

        public LiveAggregator(IPitWallConfiguration configuration) {
            _configuration = configuration;
        }

        public void SetSessionTimeLeft(string sessionTimeLeft)
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

        public void SetLaptime(string original)
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
                PilotName = _configuration.PilotName,
                LaptimeSeconds = _laptimeSeconds,
                AvgWetness = _avgWetness,
                AirTemperature = _airTemperature,
                TyresWear = new TyresWear()
                {
                    FrontLeftWear = _frontLeftTyreWear,
                    FrontRightWear = _frontRightTyreWear,
                    ReartLeftWear = _rearLeftTyreWear,
                    RearRightWear = _rearRightTyreWear,
                },
                TyresTemperatures = new TyresTemperatures()
                {
                    /**
                     * Idea: add the inner/middle/outer temperature.
                     * */
                    FrontLeftTemp = _frontLeftTyreTemp,
                    FrontRightTemp = _frontRightTyreTemp,
                    RearLeftTemp = _rearLeftTyreTemp,
                    RearRightTemp = _rearRightTyreTemp
                },
                SimerKey = _configuration.PersonalKey
            };
        }

        #region tyre wear

        public void SetFrontLeftTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _frontLeftTyreWear = tyreWearValue;

            SetDirty();
        }

        public void SetFrontRightTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _frontRightTyreWear = tyreWearValue;

            SetDirty();
        }

        public void SetRearLeftTyreWear(double? tyreWearValue)
        {
            if (!tyreWearValue.HasValue)
            {
                return;
            }

            _rearLeftTyreWear = tyreWearValue;

            SetDirty();
        }

        public void SetRearRightTyreWear(double? tyreWearValue)
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

        public void SetFrontLeftTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _frontLeftTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void SetFrontRightTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _frontRightTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void SetRearLeftTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _rearLeftTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void SetRearRightTyreTemperature(double? tyreTempValue)
        {
            if (!tyreTempValue.HasValue)
            {
                return;
            }

            _rearRightTyreTemp = tyreTempValue;

            SetDirty();
        }

        public void SetAvgWetness(double? data)
        {
            if (!data.HasValue)
            {
                return;
            }
            _avgWetness = data;

            SetDirty();
        }

        public void SetAirTemperature(double? data)
        {
            if (!data.HasValue)
            {
                return;
            }

            _airTemperature = data;

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
