# Pit Wall Acquisition Plugin

This plugin is to be used with simhub to forward data to a WebApi.

## Key code items

### LiveAggregator

The LiveAggregator classes is a builder like class to create the data and hydrate the datastructure to be sent to an API.

This class is performance sensitive. Each data assignation execution time is tested.

### PluginManagerWrapper

This class is a wrapper around Simhub plugin manager to allow mapping testing and give an opportunity to format and convert data to proper type.

### WebApiForwarderService

This class is a technical implmentation that uses the two above classes to send data to the target API.

### WebApiForwarder

The adapter to integrate the plugin to simhub.

## Coding

### How to add new telemetry fields in code ?

- Update PluginManagerWrapper and IPluginRecordRepository to expose the field from simhub plugin manager
- Update LiveAggregator add the new performance counter to liveAggregator.

### Limitations

- Autofac should not be updated because it has a dependencies in commong with Simhub.
