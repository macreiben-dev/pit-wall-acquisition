using NFluent;
using NSubstitute;
using System;
using PitWallAcquisitionPlugin.Acquisition.Telemetries.Aggregators;
using PitWallAcquisitionPlugin.Acquisition.Telemetries.Mappers;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Mappers
{
    public class LiveMapperTest
    {
        private ITelemetryLiveAggregator _aggregator;
        private IPluginRecordRepository _adapter;
        private Func<IPluginRecordRepository, string> _sourceSelector;
        private Action<ITelemetryLiveAggregator, string> _setter;

        public LiveMapperTest() {
            _aggregator = Substitute.For<ITelemetryLiveAggregator>();
            _adapter = Substitute.For<IPluginRecordRepository>();

            Func<IPluginRecordRepository, string> sourceSelector = (r) => r.LastLaptime;

            Action<ITelemetryLiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            _sourceSelector = sourceSelector;

            _setter = setter;
        }

        [Fact]
        public void WHEN_sourceSelector_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LiveTelemetryMapper<string>(null, _setter))
                .Throws<ArgumentNullException>();
        }

        [Fact]
        public void WHEN_setter_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LiveTelemetryMapper<string>(_sourceSelector, null))
                .Throws<ArgumentNullException>();
        }

        [Fact]
        public void WHEN_adapter_isNull_THEN_fail()
        {
            Func<IPluginRecordRepository, string> sourceSelector = (r) => r.LastLaptime;

            Action<ITelemetryLiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            var target = new LiveTelemetryMapper<string>(
                    sourceSelector,
                    setter
                );

            Check.ThatCode(() => target.Set(null, _aggregator))
                .Throws<ArgumentNullException>();   
        }

        [Fact]
        public void WHEN_aggregator_isNull_THEN_fail()
        {
            Func<IPluginRecordRepository, string> sourceSelector = (r) => r.LastLaptime;

            Action<ITelemetryLiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            var target = new LiveTelemetryMapper<string>(
                    sourceSelector,
                    setter
                );

            Check.ThatCode(() => target.Set(_adapter, null))
                .Throws<ArgumentNullException>();
        }
    }
}
