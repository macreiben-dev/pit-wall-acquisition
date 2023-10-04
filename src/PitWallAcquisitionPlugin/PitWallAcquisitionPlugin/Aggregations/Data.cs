namespace FuelAssistantMobile.DataGathering.SimhubPlugin.Aggregations
{
    public sealed class Data : IData
    {
        public Data()
        {
            TyresWear = new Tyres();
        }

        public string PilotName { get; set; }

        public double? LaptimeSeconds { get; set; }

        public ITyres TyresWear { get; set; }

        public string SessionTimeLeft { get; set; } = string.Empty;
    }
}
