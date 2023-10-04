namespace PitWallAcquisitionPlugin.Aggregations
{
    public struct TelemetryDoubleValue
    {
        private double? _telemetryValue;

        public TelemetryDoubleValue(double? telemetryValue)
        {
            if(!telemetryValue.HasValue)
            {
                _telemetryValue = null;
            }
            _telemetryValue = telemetryValue;
        }

        public double? Value => _telemetryValue;

        public bool HasValue => _telemetryValue.HasValue;
    } 
}
