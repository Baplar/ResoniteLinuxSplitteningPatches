# Native setup - Proton

_All of the paths in the code snippets below assume your Steam library is setup at the default location`$HOME/.local/share/Steam/steamapps`. If your setup is different, adapt the paths accordingly._

- (Optional) Make a backup of your current Proton prefix, to be able to restore your release environment
```sh
cd $HOME/.local/share/Steam/steamapps/compatdata/
cp -r 2519830{,-release}
```
- Switch to the prerelease beta on Steam
- Launch Resonite once from Steam to pre-heat the Proton prefix and install the .NET dependencies (it will eventually crash after installing the dependencies, this is expected)
- Edit the file `Resonite.runtimeconfig.json` to remove the WindowsDesktop framework dependency
```patch
--- $HOME/.local/share/Steam/steamapps/common/Resonite/Resonite.runtimeconfig.json	2025-07-16 09:44:51.509893740 +0200
+++ $HOME/.local/share/Steam/steamapps/common/Resonite/Resonite.runtimeconfig.json	2025-07-16 12:34:45.303342913 +0200
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
cd $HOME/.local/share/Steam/steamapps/common/Resonite
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --channel 9.0 --runtime dotnet --install-dir dotnet-runtime
```
- Open the Steam properties of Resonite, and set the launch options
```sh
dotnet-runtime/dotnet Resonite.dll -DataPath "$HOME/Resonite/Prerelease/Data" -CachePath "$HOME/Resonite/Prerelease/Cache" > "Logs/$(hostname) - linux-dotnet - $(date +"%F %H_%M_%S").log" 2>&1 # %command%
```
- This launch options requires that the `Logs` folder already exists.
  If you already launched the game before the pre-release, it should already be there.
  If it does not exist yet, you need to create it:
```sh
cd $HOME/.local/share/Steam/steamapps/common/Resonite
mkdir -p Logs
```
- In the Resonite install directory, move all of the contents of the `Renderer` folder into a `Renderite` subfolder
```sh
cd $HOME/.local/share/Steam/steamapps/common/Resonite/Renderer
mkdir -p Renderite
mv -b MonoBleedingEdge Renderite.Renderer* Unity* Renderite
```
- Create the modified renderer script to run the Proton version that you selected for Resonite in Steam
```sh
cd $HOME/.local/share/Steam/steamapps/common/Resonite/Renderer
cat > Renderite.Renderer.sh <<'EOF'
#!/usr/bin/env bash
cd "./Renderite" || exit
PROTON_BIN="$HOME/.local/share/Steam/steamapps/common/Proton - Experimental/proton"
IFS_BACKUP="$IFS"
IFS=":"
for COMPAT_PATH in $STEAM_COMPAT_TOOL_PATHS; do
    if [ -f "$COMPAT_PATH/proton" ]; then
        PROTON_BIN="$COMPAT_PATH/proton"
    fi 
done
IFS="$IFS_BACKUP"
echo "Using proton binary: $PROTON_BIN"
exec "$PROTON_BIN" run Renderite.Renderer.exe "$@"
EOF
chmod +x Renderite.Renderer.sh
cp Renderite.Renderer.sh Renderite.Renderer.exe
```
- Launch the game from Steam, it should start!

## Patches

You should also install additional performance patches in order to get a good experience.

This is a bit more experimental, as it replaces some of the DLLs of the game.
But this is a real boon for getting more FPS, and some variation of these changes will most likely end up in the official release.
This is also currently required in order to try to run the game in VR mode.

- Download [the latest release of the native patches](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.8/NativeProtonPatches.zip).
- Extract the contents of the downloaded zip file into your Resonite install folder.
