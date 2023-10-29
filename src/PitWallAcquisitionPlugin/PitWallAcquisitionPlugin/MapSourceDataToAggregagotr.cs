using FuelAssistantMobile.DataGathering.SimhubPlugin;
using PitWallAcquisitionPlugin.Aggregations;

namespace PitWallAcquisitionPlugin
{
    internal static class MapSourceDataToAggregagtor
    {
        public static void UpdateAggregatorNow(ILiveAggregator aggregator, IPluginRecordRepository racingDataRepository)
        {
            aggregator.SetSessionTimeLeft(racingDataRepository.SessionTimeLeft);

            aggregator.SetLaptime(racingDataRepository.LastLaptime);

            aggregator.SetFrontLeftTyreWear(racingDataRepository.TyreWearFrontLeft);
            aggregator.SetFrontRightTyreWear(racingDataRepository.TyreWearFrontRight);
            aggregator.SetRearLeftTyreWear(racingDataRepository.TyreWearRearLeft);
            aggregator.SetRearRightTyreWear(racingDataRepository.TyreWearRearRight);

            aggregator.SetFrontLeftTyreTemperature(racingDataRepository.TyreFrontLeftTemperature.Average);

            aggregator.SetFrontRightTyreTemperature(racingDataRepository.TyreFrontRightTemperature.Average);

            aggregator.SetRearLeftTyreTemperature(racingDataRepository.TyreRearLeftTemperature.Average);

            aggregator.SetRearRightTyreTemperature(racingDataRepository.TyreRearRightTemperature.Average);

            aggregator.SetAirTemperature(racingDataRepository.AirTemperature);

            aggregator.SetAvgWetness(racingDataRepository.AvgRoadWetness);
        }
    }
}