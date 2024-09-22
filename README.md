# Pitwall Acquisition Plugin

![Pitwall Header](./docs/assets/pitwall-header.png)

This is a plugin that works with Simhub. It forward data to a web platform for you to plan strategy and analyze the car behaviour.

Since it's a SimHub plugin, you can use it with various simulator.

## Versions
- [Download latest Stable version](https://github.com/macreiben-dev/pit-wall-acquisition/releases/tag/Live-20240917.2-852)
- [Download Release Candidate version](https://github.com/macreiben-dev/pit-wall-acquisition/releases/tag/RC-20240605.1-820)

## Pre requisites

- Simhub 9.x installed

To be able to work with the data, please see [pit-wall-api](https://github.com/macreiben-dev/pit-wall-api) documentation.

- Gary Swallow plugin installed in SimHub

To be able to have the pitting information you need download [Simhub Gary Swallow plugin](https://www.overtake.gg/downloads/simhub-tv-style-side-scrolling-leaderboards-timings-sidescreen.18746/)

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

- Then plugins

![Got to plugins](./docs//assets/Slide3.PNG)

- Ensure plugins is activated

![Ensure plugins is activated](./docs//assets/Slide4.PNG)

You can check the checbox "Show in the left main menu" to access the plugin directly.

### Configuration

- Open the plugin configuration screen
- Set the pilot name and car name

![Set the pilot name and car name](./docs/assets/SETUP_Slide1.PNG)

This two informations will be use to group the metrics by car and pilots.

It is recommended to have a unique pilot name by user. Car name can be shared. Doing so will enable you to support driver swap scenarii.

- Set the server address and personal key

![Set the server address and personal key](./docs/assets/SETUP_Slide2.PNG)

Personal key will be checked each time a user tries to send metrics. One server knows one personal key.

- Click save button

![Click save button](./docs/assets/SETUP_Slide3.PNG)

Save button updates the configuration. You do not need to restart Simhub.

- Test Connectivity button

Tries to contact the server without authentication. This is to avoid personal key brut force attack.

- OK : server is reachable
- KO : server is unreachable

To setup the server see [pitwall api](https://github.com/macreiben-dev/pit-wall-api)
