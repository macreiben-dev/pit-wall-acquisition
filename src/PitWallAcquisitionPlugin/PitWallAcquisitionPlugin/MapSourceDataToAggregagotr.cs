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
                new LiveMapper(r => r.AirTemperature, (a, data) => a.SetAirTemperature(data)),
                new LiveMapper(r => r.AvgRoadWetness, (a, data) => a.SetAvgWetness(data)),

                // ----

                new LiveMapper(r => r.TyreFrontLeftTemperature.Average, (a, data) => a.SetFrontLeftTyreTemperature(data)),
                new LiveMapper(r => r.TyreRearLeftTemperature.Average, (a, data) => a.SetRearLeftTyreTemperature(data)),
                new LiveMapper(r => r.TyreFrontRightTemperature.Average, (a, data) => a.SetFrontRightTyreTemperature(data)),
                new LiveMapper(r => r.TyreRearRightTemperature.Average, (a, data) => a.SetRearRightTyreTemperature(data)),
                
                // ----

                new LiveMapper(r => r.TyreWearFrontLeft, (a, data) => a.SetFrontLeftTyreWear(data)),
                new LiveMapper(r => r.TyreWearRearLeft, (a, data) => a.SetRearLeftTyreWear(data)),
                new LiveMapper(r => r.TyreWearFrontRight, (a, data) => a.SetFrontRightTyreWear(data)),
                new LiveMapper(r => r.TyreWearRearRight, (a, data) => a.SetRearRightTyreWear(data)),
            };

            foreach (var config in allConfiguration)
            {
                config.Set(racingDataRepository, aggregator);
            }

            aggregator.SetSessionTimeLeft(racingDataRepository.SessionTimeLeft);

            aggregator.SetLaptime(racingDataRepository.LastLaptime);
        }
    }
}