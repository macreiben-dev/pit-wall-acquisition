# Pit Wall Acquisition Plugin

This plugin is to be used with simhub to forward data to a WebApi. This WebApi then exposes an endpoint to feed a prometheus database.

[Download latest version](https://github.com/macreiben-dev/pit-wall-acquisition/releases/tag/Live-20231126.5)

## Pre requisites

- Simhub 9.x installed

To be able to work with the data, please see [pit-wall-api](https://github.com/macreiben-dev/pit-wall-api) documentation.

## How does it work ?

![Global schematic](./docs/assets/pitwall_prez.png)

Any simulator supported by [SimHub](https://www.simhubdash.com/) can be used with the plugin.

## Setup the plugin

- Download the latest release
- Unzip the binaries downloaded archive
- Copy the files in Simhub installation directory
  - If you are updating from an existing version,
    - Ensure Simhub is not running
    - Say yes to replace existing file with the new files
- Start Simhub

### First installation

- Go to Settings

![Go to Settings](./docs/assets/Slide2.png)
