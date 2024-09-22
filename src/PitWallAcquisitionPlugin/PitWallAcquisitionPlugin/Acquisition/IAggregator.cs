namespace PitWallAcquisitionPlugin.Acquisition
{
    internal interface IAggregator
    {
        bool IsDirty { get; }

        ISendableData AsData();

        void Clear();

        void UpdateAggregator(IPluginRecordRepository racingDataRepository);
    }
}
