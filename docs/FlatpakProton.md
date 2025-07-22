# Native setup - Flatpak

The setup to get Resonite running on Flatpak is similar to the one for the [Native Proton](NativeProton.md) setup,
with a couple adjustments needed to handle the special case of Flatpak.

_All of the paths in the code snippets below assume:_
- _Your Steam install folder is setup at the default location `$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/`_
- _Your Steam library is located at the default location as well_

_If your setup is different, adapt the paths accordingly._


- (Optional) Make a backup of your current Proton prefix, to be able to restore your release environment
```sh
cd $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/compatdata/
cp -r 2519830{,-release}
```
- Switch to the prerelease beta on Steam
- Launch Resonite once from Steam to pre-heat the Proton prefix and install the .NET dependencies (it will eventually crash after installing the dependencies, this is expected)
- Edit the file `Resonite.runtimeconfig.json` to remove the WindowsDesktop framework dependency
```patch
--- $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Resonite/Resonite.runtimeconfig.json	2025-07-16 09:44:51.509893740 +0200
+++ $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Resonite/Resonite.runtimeconfig.json	2025-07-16 12:34:45.303342913 +0200
@@ -5,10 +5,6 @@
       {
         "name": "Microsoft.NETCore.App",
         "version": "9.0.0"
-      },
-      {
-        "name": "Microsoft.WindowsDesktop.App",
-        "version": "9.0.0"
       }
     ],
     "configProperties": {
```
- Download the .NET runtime in your Resonite install folder
```sh
cd $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Resonite
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 9.0 --runtime dotnet --install-dir dotnet-runtime
```
- Open the Steam properties of Resonite, and set the launch options
```sh
dotnet-runtime/dotnet Resonite.dll > "Logs/$(hostname) - linux-dotnet - $(date +"%F %H_%M_%S").log" 2>&1 # %command%
```
- In the Resonite install directory, move all of the contents of the `Renderer` folder into a `Renderite` subfolder
```sh
cd $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Resonite/Renderer
mkdir -p Renderite
mv -b MonoBleedingEdge Renderite.Renderer* Unity* Renderite
```
- Find the location of your preferred Proton version. For example, Proton Experimental is usually located in
`$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Proton\ -\ Experimental/proton`
- Create the modified renderer script, using the location of the proton executable found in the previous step
_(mind the double backslashes for escaping spaces!)_
```sh
cd $HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Resonite/Renderer
cat > Renderite.Renderer.exe <<EOF
#!/usr/bin/env bash
cd "./Renderer/Renderite" || exit
$HOME/.var/app/com.valvesoftware.Steam/.local/share/Steam/steamapps/common/Proton\\ -\\ Experimental/proton run Renderite.Renderer.exe "\$@"
EOF
chmod +x Renderite.Renderer.exe
```
- Launch the game from Steam, it should start!

## Patches

You should also install additional performance patches in order to get a good experience.

This is a bit more experimental, as it replaces some of the DLLs of the game.
But this is a real boon for getting more FPS, and some variation of these changes will most likely end up in the official release. 

- Download [the latest release of the native patches](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.3/NativeProtonPatches.zip).
- Extract the contents of the downloaded zip file into your Resonite install folder.
