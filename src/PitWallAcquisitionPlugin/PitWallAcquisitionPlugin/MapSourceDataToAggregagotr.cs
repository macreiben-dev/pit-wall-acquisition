using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations;
using PitWallAcquisitionPlugin.Aggregations.v2;

namespace PitWallAcquisitionPlugin
{
    public static class MapSourceDataToAggregagtor
    {
        public static void UpdateAggregatorNow(ILiveAggregator aggregator, IPluginRecordRepository racingDataRepository)
        {
            var allConfiguration = new[]
            {
                LiveMapper.GetInstance(r => r.AirTemperature, (a, data) => a.SetAirTemperature(data)),
                LiveMapper.GetInstance(r => r.AvgRoadWetness, (a, data) => a.SetAvgWetness(data)),

                // ----

                LiveMapper.GetInstance(r => r.TyreFrontLeftTemperature.Average, (a, data) => a.SetFrontLeftTyreTemperature(data)),
                LiveMapper.GetInstance(r => r.TyreRearLeftTemperature.Average, (a, data) => a.SetRearLeftTyreTemperature(data)),
                LiveMapper.GetInstance(r => r.TyreFrontRightTemperature.Average, (a, data) => a.SetFrontRightTyreTemperature(data)),
                LiveMapper.GetInstance(r => r.TyreRearRightTemperature.Average, (a, data) => a.SetRearRightTyreTemperature(data)),
                
                // ----

                LiveMapper.GetInstance(r => r.TyreWearFrontLeft, (a, data) => a.SetFrontLeftTyreWear(data)),
                LiveMapper.GetInstance(r => r.TyreWearRearLeft, (a, data) => a.SetRearLeftTyreWear(data)),
                LiveMapper.GetInstance(r => r.TyreWearFrontRight, (a, data) => a.SetFrontRightTyreWear(data)),
                LiveMapper.GetInstance(r => r.TyreWearRearRight, (a, data) => a.SetRearRightTyreWear(data)),

                // ----
                
                LiveMapper.GetInstance(r => r.LastLaptime, (a, data) => a.SetLaptime(data)),
                LiveMapper.GetInstance(r => r.SessionTimeLeft, (a, data) => a.SetSessionTimeLeft(data))
            };

            foreach (var config in allConfiguration)
            {
                config.Set(racingDataRepository, aggregator);
            }
        }
    }
}