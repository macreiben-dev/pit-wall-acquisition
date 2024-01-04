using PitWallAcquisitionPlugin.Aggregations.Telemetries.Mappers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PitWallAcquisitionPlugin
{
    public sealed class MappingConfigurationRepository : IMappingConfigurationRepository 
    {
        private readonly ILiveMapper[] _allConfiguration;

        public MappingConfigurationRepository() {

            _allConfiguration = new[]
              {
                LiveMapperFactory.GetInstance(r => r.AirTemperature, (a, data) => a.SetAirTemperature(data)),
                LiveMapperFactory.GetInstance(r => r.AvgRoadWetness, (a, data) => a.SetAvgWetness(data)),
                LiveMapperFactory.GetInstance(r => r.TraceTemperature, (a, data) => a.SetTrackTemperature(data)),

                LiveMapperFactory.GetInstance(r => r.Fuel, (a, data) => a.SetFuel(data)),
                LiveMapperFactory.GetInstance(r => r.MaxFuel, (a, data) => a.SetMaxFuel(data)),
                LiveMapperFactory.GetInstance(r => r.ComputedLastLapConsumption, (a, data) => a.SetComputedLastLapConsumption(data)),
                LiveMapperFactory.GetInstance(r => r.ComputedLiterPerLaps, (a, data) => a.SetComputedLiterPerLaps(data)),
                LiveMapperFactory.GetInstance(r => r.ComputedRemainingLaps, (a, data) => a.SetComputedRemainingLaps(data)),
                LiveMapperFactory.GetInstance(r => r.ComputedRemainingTime, (a, data) => a.SetComputedRemainingTime(data)),

                // ----

                LiveMapperFactory.GetInstance(r => r.TyreFrontLeftTemperature.Average, (a, data) => a.SetFrontLeftTyreTemperature(data)),
                LiveMapperFactory.GetInstance(r => r.TyreRearLeftTemperature.Average, (a, data) => a.SetRearLeftTyreTemperature(data)),
                LiveMapperFactory.GetInstance(r => r.TyreFrontRightTemperature.Average, (a, data) => a.SetFrontRightTyreTemperature(data)),
                LiveMapperFactory.GetInstance(r => r.TyreRearRightTemperature.Average, (a, data) => a.SetRearRightTyreTemperature(data)),


                // ----

                LiveMapperFactory.GetInstance(r => r.TyreWearFrontLeft, (a, data) => a.SetFrontLeftTyreWear(data)),
                LiveMapperFactory.GetInstance(r => r.TyreWearRearLeft, (a, data) => a.SetRearLeftTyreWear(data)),
                LiveMapperFactory.GetInstance(r => r.TyreWearFrontRight, (a, data) => a.SetFrontRightTyreWear(data)),
                LiveMapperFactory.GetInstance(r => r.TyreWearRearRight, (a, data) => a.SetRearRightTyreWear(data)),

                // ----
                
                LiveMapperFactory.GetInstance(r => r.LastLaptime, (a, data) => a.SetLaptime(data)),
                LiveMapperFactory.GetInstance(r => r.SessionTimeLeft, (a, data) => a.SetSessionTimeLeft(data))
            };
        } 

        public IEnumerator<ILiveMapper> GetEnumerator()
        {
            return _allConfiguration.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _allConfiguration.GetEnumerator();
        }
    }
}
