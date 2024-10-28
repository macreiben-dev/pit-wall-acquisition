namespace PitWallAcquisitionPlugin.Acquisition.Leadeboards
{
    internal class DummyData : ISendableData
    {
        public string SimerKey { get; set; }

        public string PilotName { get; set; }

        public string CarName { get; set; }

        public object AsData()
        {
            return new
            {
                CarName = string.Empty,
                PilotName = string.Empty,
                SimerKey = string.Empty,
                SomeValue = "hello_world"
            };
        }
    }
}
