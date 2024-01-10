using PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PitWallAcquisitionPlugin
{
    internal sealed class MappingConfigurationRepository : IMappingConfigurationRepository 
    {
        private readonly ILiveTelemetryMapper[] _allConfiguration;

        public MappingConfigurationRepository() {

            _allConfiguration = new[]
              {
                LiveTelemetryMapperFactory.GetInstance(r => r.AirTemperature, (a, data) => a.SetAirTemperature(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.AvgRoadWetness, (a, data) => a.SetAvgWetness(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TraceTemperature, (a, data) => a.SetTrackTemperature(data)),

                LiveTelemetryMapperFactory.GetInstance(r => r.Fuel, (a, data) => a.SetFuel(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.MaxFuel, (a, data) => a.SetMaxFuel(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.ComputedLastLapConsumption, (a, data) => a.SetComputedLastLapConsumption(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.ComputedLiterPerLaps, (a, data) => a.SetComputedLiterPerLaps(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.ComputedRemainingLaps, (a, data) => a.SetComputedRemainingLaps(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.ComputedRemainingTime, (a, data) => a.SetComputedRemainingTime(data)),

                // ----

                LiveTelemetryMapperFactory.GetInstance(r => r.TyreFrontLeftTemperature.Average, (a, data) => a.SetFrontLeftTyreTemperature(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreRearLeftTemperature.Average, (a, data) => a.SetRearLeftTyreTemperature(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreFrontRightTemperature.Average, (a, data) => a.SetFrontRightTyreTemperature(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreRearRightTemperature.Average, (a, data) => a.SetRearRightTyreTemperature(data)),


                // ----

                LiveTelemetryMapperFactory.GetInstance(r => r.TyreWearFrontLeft, (a, data) => a.SetFrontLeftTyreWear(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreWearRearLeft, (a, data) => a.SetRearLeftTyreWear(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreWearFrontRight, (a, data) => a.SetFrontRightTyreWear(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.TyreWearRearRight, (a, data) => a.SetRearRightTyreWear(data)),

                // ----
                
                LiveTelemetryMapperFactory.GetInstance(r => r.LastLaptime, (a, data) => a.SetLaptime(data)),
                LiveTelemetryMapperFactory.GetInstance(r => r.SessionTimeLeft, (a, data) => a.SetSessionTimeLeft(data))
            };
        } 

        public IEnumerator<ILiveTelemetryMapper> GetEnumerator()
        {
            return _allConfiguration.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _allConfiguration.GetEnumerator();
        }
    }
}
