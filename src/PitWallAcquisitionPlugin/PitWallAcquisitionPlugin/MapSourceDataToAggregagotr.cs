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
                //new LiveMapper(r => r.TyreFrontLeftTemperature.Average, (a, data) => a.SetFrontLeftTyreTemperature(data)),
            };

            foreach (var config in allConfiguration)
            {
                config.Set(racingDataRepository, aggregator);
            }

            aggregator.SetSessionTimeLeft(racingDataRepository.SessionTimeLeft);

            aggregator.SetLaptime(racingDataRepository.LastLaptime);

            //aggregator.SetAirTemperature(racingDataRepository.AirTemperature);

            //aggregator.SetAvgWetness(racingDataRepository.AvgRoadWetness);

            aggregator.SetFrontLeftTyreWear(racingDataRepository.TyreWearFrontLeft);
            aggregator.SetFrontRightTyreWear(racingDataRepository.TyreWearFrontRight);
            aggregator.SetRearLeftTyreWear(racingDataRepository.TyreWearRearLeft);
            aggregator.SetRearRightTyreWear(racingDataRepository.TyreWearRearRight);

            //aggregator.SetFrontLeftTyreTemperature(racingDataRepository.TyreFrontLeftTemperature.Average);

            //aggregator.SetFrontRightTyreTemperature(racingDataRepository.TyreFrontRightTemperature.Average);

            //aggregator.SetRearLeftTyreTemperature(racingDataRepository.TyreRearLeftTemperature.Average);

            aggregator.SetRearRightTyreTemperature(racingDataRepository.TyreRearRightTemperature.Average);
        }
    }
}