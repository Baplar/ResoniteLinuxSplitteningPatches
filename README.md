# Linux Splittening Patches

A set of tweaks for [Resonite](https://resonite.com/) to allow the pre-release Splittening builds to run on Linux.

The current pre-release can now be started on Linux! It only requires a few manual tricks.

## Native Linux build

It is possible to run the main FrooxEngine process in the native Linux .NET runtime, while only the Unity renderer will still run in Wine or Proton.

This is most likely going to be the approach that is taken for the official release of the Splittening, and thus we recommend this method.

It is possible to run this setup by using the [system-installed Wine version](docs/NativeWine.md) or by using a [Proton version for improved performance and compatibility](docs/NativeProton.md) (recommended).

- Please note that, in addition to all the features still in development, there are a couple of features that work on Windows but not yet on Linux. Notably:
  - Audio output does not work (the CSCore library only supports Windows; alternatives are being considered)
  - The clipboard OS integration can not manage rich data like images or files (copying plain text to and from Resonite *might* work, but I can not confirm yet)

### Flatpak build

It is also possible to run Resonite in the Flatpak version of Steam.

The setup is exactly identical to the one for the [native Linux Proton build](docs/NativeProton.md).

You may only need to adjust the install path of your Steam library,
which may be in `$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps`
instead of `$HOME/.local/share/Steam/steamapps` depending on your setup.

## Full Proton build

It is also possible to run all the Resonite processes [directly within Proton](docs/FullProton.md).

This method requires a bit more hacking to get working and is likely to be abandoned for the official release of the Splittening.

However, it has the benefit of not suffering from the handful of .NET libraries used by FrooxEngine that do not support Linux. Most notably, the audio systems and the clipboard work in this setup.
