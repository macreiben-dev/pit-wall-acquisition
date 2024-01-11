using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators.Models;
using PitWallAcquisitionPlugin.UI.ViewModels;
using System;
using System.Globalization;

namespace PitWallAcquisitionPlugin.Aggregations.Telemetries.Aggregators
{
    internal sealed class TelemetryLiveAggregator : ITelemetryLiveAggregator
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
        private double? _trackTemperature;

        private double? _fuel;
        private double? _maxFuel;
        private double? _computedLastLapConsumption;
        private double? _computedLiterPerLaps;
        private double? _computedRemainingLaps;
        private double? _computedRemainingTime;

        private readonly IPitWallConfiguration _configuration;
        private readonly IMappingConfigurationRepository _mappingConfiguration;

        public TelemetryLiveAggregator(IPitWallConfiguration configuration, IMappingConfigurationRepository mappingConfiguration)
        {
            _configuration = configuration;
            _mappingConfiguration = mappingConfiguration;
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

        public void SetTrackTemperature(double? data)
        {
            if (!data.HasValue)
            {
                return;
            }

            _trackTemperature = data;

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

        private void SetValueUnlessIsNull(double? data, Action<double?> setter)
        {
            if (!data.HasValue)
            {
                return;
            }

            setter(data);

            SetDirty();
        }

        public void SetFuel(double? data)
        {
            SetValueUnlessIsNull(data, (s) => _fuel = s);
        }

        public void SetMaxFuel(double? data)
        {
            SetValueUnlessIsNull(data, (s) => _maxFuel = s);
        }

        public void SetComputedLastLapConsumption(double? data)
        {
            SetValueUnlessIsNull(data, (s) => _computedLastLapConsumption = s);
        }

        public void SetComputedLiterPerLaps(double? data)
        {
            SetValueUnlessIsNull(data, (s) => _computedLiterPerLaps = s);
        }

        public void SetComputedRemainingLaps(double? data)
        {
            SetValueUnlessIsNull(data, (s) => _computedRemainingLaps = s);
        }

        public void SetComputedRemainingTime(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            var duration = TimeSpan.Parse(data, CultureInfo.InvariantCulture);

            _computedRemainingTime = duration.TotalMilliseconds / 1000;

            SetDirty();
        }

        #region IAggregator

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

        public ISendableData AsData()
        {
            return new TelemetryData
            {
                SimerKey = _configuration.PersonalKey,
                PilotName = _configuration.PilotName,
                CarName = _configuration.CarName,
                // ---------------------------------
                SessionTimeLeft = _sessionTimeLeft,
                LaptimeSeconds = _laptimeSeconds,
                AvgWetness = _avgWetness,
                AirTemperature = _airTemperature,
                TrackTemperature = _trackTemperature,
                TyresWear = new TyresWear()
                {
                    FrontLeftWear = _frontLeftTyreWear,
                    FrontRightWear = _frontRightTyreWear,
                    RearLeftWear = _rearLeftTyreWear,
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
                VehicleConsumption = new VehicleConsumption()
                {
                    Fuel = _fuel,
                    MaxFuel = _maxFuel,
                    ComputedLastLapConsumption = _computedLastLapConsumption,
                    ComputedLiterPerLaps = _computedLiterPerLaps,
                    ComputedRemainingLaps = _computedRemainingLaps,
                    ComputedRemainingTime = _computedRemainingTime
                }
            };
        }

        public void UpdateAggregator(
           IPluginRecordRepository racingDataRepository)
        {

            /**
            * Idea: we have one side where we read from plugin manager, and another 
            * in which we map the retrieved data to the aggregator.
            * 
            * I don't like big dictionary cause I like control. But I might have to
            * centralize the definition of the copy from plugin manager to racing data repo.
            * 
            * */

            foreach (var config in _mappingConfiguration)
            {
                config.Set(racingDataRepository, this);
            }
        }

        public bool IsDirty => _dirty;

        #endregion IAggregator
    }
}
