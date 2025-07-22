# Full Proton setup

_All of the paths in the code snippets below assume your Steam library is setup at the default location `$HOME/.local/share/Steam/steamapps`. If your setup is different, adapt the paths accordingly._

- (Recommended) Back up your Proton prefix by making a copy of the folder
  `$HOME/.local/share/Steam/steamapps/compatdata/2519830`
- (Optional) Delete the Proton prefix and launch the game in order to start with a fresh prefix
  (_this may prevent winetricks from failing to install .NET 4.0_)
- Setup the necessary dependencies in your Proton prefix with winetricks:
  ```sh
  WINEPREFIX="$HOME/.local/share/Steam/steamapps/compatdata/2519830/pfx" winetricks dxvk winhttp vcrun2022 dotnet48 dotnetdesktop9
  ```
  _If the installation of .NET 4.0 or 4.5 fails, you can try to delete the prefix and start again on a new fresh prefix._
- Install the prerelease version of [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader/releases) in the root of your Resonite install folder. This should be at `$HOME/.local/share/Steam/steamapps/common/Resonite` for a default install
- Install [MelonLoader](https://melonwiki.xyz/) in the `Renderer` sub-folder of your Resonite install folder. This should be at `$HOME/.local/share/Steam/steamapps/common/Resonite/Renderer` for a default install.
- Download [the latest release of the proton patches](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.3/FullProtonPatches.zip).
- Extract the contents of the downloaded zip file into your Resonite install folder.
- Set your launch options to run both ResoniteModLoader and MelonLoader
```
WINEDLLOVERRIDES="version=n,b" %command% -LoadAssembly Libraries/ResoniteModLoader.dll > "Logs/$(hostname) - linux-dotnet - $(date +"%F %H_%M_%S").log" 2>&1
```
- Start the game. If you want to verify that the mod is working you can check your Resonite logs.

## Credits

Thanks to [AdamSki2003](https://git.adamski2003.lol/adam/ResoniteDESFix)
for providing the implementation of DES used on the FrooxEngine side,
as well as the skeleton of that mod.

Thanks to [Bredo](https://github.com/bredo228/Hardware.Info)
for providing the workaround for the hardware information gathering method
(in particular for counting CPU cores) causing a crash on Wine.

The rest of this rough mode set is brought to you by [Baplar](https://github.com/baplar).
