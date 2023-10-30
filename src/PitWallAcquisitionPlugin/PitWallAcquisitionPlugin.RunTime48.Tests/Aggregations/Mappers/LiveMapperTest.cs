using FuelAssistantMobile.DataGathering.SimhubPlugin;
using NFluent;
using NSubstitute;
using PitWallAcquisitionPlugin.Aggregations.Aggregators;
using PitWallAcquisitionPlugin.Aggregations.Mappers;
using System;
using Xunit;

namespace PitWallAcquisitionPlugin.RunTime48.Tests.Aggregations.Mappers
{
    public class LiveMapperTest
    {
        private ILiveAggregator _aggregator;
        private IPluginRecordRepository _adapter;
        private Func<IPluginRecordRepository, string> _sourceSelector;
        private Action<ILiveAggregator, string> _setter;

        public LiveMapperTest() {
            _aggregator = Substitute.For<ILiveAggregator>();
            _adapter = Substitute.For<IPluginRecordRepository>();

            Func<IPluginRecordRepository, string> sourceSelector = (r) => r.LastLaptime;

            Action<ILiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            _sourceSelector = sourceSelector;

            _setter = setter;
        }

        [Fact]
        public void WHEN_sourceSelector_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LiveMapper<string>(null, _setter))
                .Throws<ArgumentNullException>();
        }

        [Fact]
        public void WHEN_setter_isNull_THEN_fail()
        {
            Check.ThatCode(() => new LiveMapper<string>(_sourceSelector, null))
                .Throws<ArgumentNullException>();
        }

        [Fact]
        public void WHEN_adapter_isNull_THEN_fail()
        {
            Func<IPluginRecordRepository, string> sourceSelector = (r) => r.LastLaptime;

            Action<ILiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            var target = new LiveMapper<string>(
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

            Action<ILiveAggregator, string> setter = (a, counter) => a.SetLaptime(counter);

            var target = new LiveMapper<string>(
                    sourceSelector,
                    setter
                );

            Check.ThatCode(() => target.Set(_adapter, null))
                .Throws<ArgumentNullException>();
        }
    }
}
