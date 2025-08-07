# Linux Splittening Patches

A set of tweaks for [Resonite](https://resonite.com/) to allow the pre-release Splittening builds to run as well as possible on Linux.

The current pre-release can now be started on Linux almost out of the box! It only requires a few manual tricks.

## Native Linux build

It is possible to run the main FrooxEngine process in the native Linux .NET runtime, while only the Unity renderer will still run in Wine or Proton.

This is going to be the approach that is taken for the official release of the Splittening, and thus we recommend this method.

This works almost out of the box, but there is still a small bug with Steam using the incorrect line break format for the Linux shell scripts.
This can be easily fixed by running a simple command in the Resonite install directory:
```sh
dos2unix Resonite.sh dotnet-install.sh
```

- Please note that, in addition to all the known bugs still being fixed on the pre-release build, there are a couple of features that work on Windows but not yet on Linux. Notably:
  - Audio output does not work, the CSCore library currently used only supports Windows. SDL is currently being implemented to replace it, which will bring a cross-platform audio backend into Resonite.
  - The clipboard OS integration does not work either. Work has not started on this aspect quite yet as of writing this documentation (2025-08-07), but it might end up being implemented using SDL as well.

### VR mode
As of today, VR mode crashes almost instantly due to a library only being available on Windows (SRAnipal).
This can be solved by using [this mod](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.9/LinuxSplitteningPatches.zip).

This mod requires having installed a version of the [Resonite Mod Loader](https://github.com/resonite-modding-group/ResoniteModLoader) compatible with post-Splittening Resonite.
If you do not have it installed already, we recommend you to use [our updated build of the mod loader](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.9/RML_Splittening.zip).

### YouTube videos
As of today, the Linux version of Resonite still uses an old path for the location of yt-dlp, the tool used for loading YouTube videos.
In order for videos to load, you need to:
- Download [the latest version of yt-dlp](https://github.com/yt-dlp/yt-dlp-nightly-builds/releases/latest/download/yt-dlp)
- Place it inside the Resonite install folder and rename it `youtube-dl`

## Full Proton build

It is also possible to run all the Resonite processes [directly within Proton](docs/FullProton.md).

This method requires a bit more hacking to get working and is likely to be abandoned for the official release of the Splittening.

However, it has the benefit of not suffering from the handful of .NET libraries used by FrooxEngine that do not support Linux. Most notably, the audio systems and the clipboard work in this setup.
