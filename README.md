# Linux Splittening Patches

A set of mods for [Resonite](https://resonite.com/) that applies patches to allow the pre-release Splittening builds to run on Linux through Proton.

## Installation
1. (Recommended) Back up your Proton prefix by making a copy of the folder
   `$HOME/.local/share/Steam/steamapps/compatdata/2519830`
1. (Optional) Delete the Proton prefix and launch the game in order to start with a fresh prefix
   (_this may prevent winetricks from failing to install .NET 4.0_)
1. Setup the necessary dependencies in your Proton prefix with winetricks:
   ```
   WINEPREFIX="$HOME/.local/share/Steam/steamapps/compatdata/2519830/pfx" winetricks dxvk winhttp vcrun2022 dotnet48 dotnetdesktop9
   ```
   _If the installation of .NET 4.0 or 4.5 fails, you can try to delete the prefix and start again on a new fresh prefix._
4. Install [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) in the root of your Resonite install folder. This should be at `$HOME/.local/share/Steam/steamapps/common/Resonite` for a default install
2. Install [MelonLoader](https://melonwiki.xyz/) in the `Renderer` sub-folder of your Resonite install folder. This should be at `$HOME/.local/share/Steam/steamapps/common/Resonite/Renderer` for a default install.
6. Download [the latest release of the mod](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/latest/download/ResoniteLinuxSplitteningPatches.zip).
7. Extract the contents of the downloaded zip file into your Resonite install folder.
8. Start the game. If you want to verify that the mod is working you can check your Resonite logs.

## Credits

Thanks to [AdamSki2003](https://git.adamski2003.lol/adam/ResoniteDESFix)
for providing the implementation of DES used on the FrooxEngine side,
as well as the skeleton of that mod.

Thanks to [Bredo](https://github.com/bredo228/Hardware.Info)
for providing the workaround for the hardware information gathering method
(in particular for counting CPU cores) causing a crash on Wine.

The rest of this rough mode set is brought to you by [Baplar](https://github.com/baplar).
