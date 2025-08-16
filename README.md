# Linux Splittening Patches

A repository of information for [Resonite](https://resonite.com/) to allow the pre-release Splittening builds to run as well as possible on Linux.

The current pre-release can now be started on Linux out of the box! It only requires a few manual tricks for very specific use cases.

## Native Linux build

It is possible to run the main FrooxEngine process in the native Linux .NET runtime, while only the Unity renderer will still run in Wine or Proton.

It works almost out of the box today, and it is going to be the approach that is taken for the official release of the Splittening, thus we strongly recommend this method.

Make sure to explicitly select a Proton version in the Compatibility menu of the Steam properties for Resonite. We recommend the latest stable (9.0-4 as of today) or Experimental.
![Steam Library > Resonite > Right click > Properties > Compatibility > Force the use of a specific Steam Play compatibility tool > Select a Proton version of your choice](docs/Proton%20compatibility%20tool.png)

### Remaining Linux-specific issues

#### Clipboard
The clipboard does not work on Linux yet, because it currently depends on Windows-specific API functions provided by the .NET runtime.
[SDL is currently being implemented to support this feature on Linux](https://github.com/Yellow-Dog-Man/Resonite-Issues/issues/4974).

#### Visemes
Visemes (voice-controlled lip animations) can not be computed on Linux.
Linux players can see visemes on Windows players, and Windows players can see them on everyone, but Linux players can not see their own visemes, nor those of other Linux players.
[Community efforts are being made to find or provide a cross-platform alternative to the Windows-only OVRLipSync library](https://github.com/Yellow-Dog-Man/Resonite-Issues/issues/5151).


## Full Proton build *(advanced users)*

Although we strongly discourage you to do it unless you know exactly what you’re doing, it is technically still possible to run the main Resonite process [directly within Proton](docs/FullProton.md).

This method requires a bit of tinkering to get working, and is going to be abandoned for the official release of the Splittening.

However, in the meantime, it has the small benefit of not suffering from the handful of .NET libraries used by FrooxEngine that do not support Linux. Notably, the clipboard and visemes work in this setup.
