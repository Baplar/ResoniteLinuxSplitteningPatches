# Full Proton setup

Although we strongly discourage you to do it unless you know exactly what you’re doing, it is technically still possible to run the main Resonite process [directly within Proton](docs/FullProton.md).

This method requires a bit of tinkering to get working, and is going to be abandoned for the official release of the Splittening.

However, in the meantime, it has the small benefit of not suffering from the handful of .NET libraries used by FrooxEngine that do not support Linux. Notably, the clipboard and visemes work in this setup.

_All of the paths in the code snippets below assume your Steam library is setup at the default location `$HOME/.local/share/Steam/steamapps`. If your setup is different, adapt the paths accordingly._

- Install a version of the [Resonite Mod Loader](https://github.com/resonite-modding-group/ResoniteModLoader) compatible with post-Splittening Resonite. We recommend you to use [our updated build of the mod loader](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.10/RML_Splittening.zip).
- Download and extract [the LinuxSplitteningPatches mod](https://github.com/Baplar/ResoniteLinuxSplitteningPatches/releases/download/v0.1.10/FullProtonSplitteningPatches.zip). This is necessary to patch a couple of functions in FrooxEngine which do not behave well under Proton (DES cipher, uTouchInjection, Unity renderer watchdog, Hardware.Info CPU core info fetcher).
- Edit Resonite.sh to launch the main DLL under Proton instead of system dotnet 
```sh
patch -u << "EOF"
--- Resonite.sh	2025-08-08 00:22:39.530780185 +0200
+++ Resonite.sh	2025-08-08 01:38:14.434693170 +0200
@@ -93,7 +93,19 @@
 
 	# ~ Launch Resonite! :) ~
 
-	dotnet Renderite.Host.dll "$@"
+	# dotnet Renderite.Host.dll "$@"
+
+	PROTON_BIN="$HOME/.local/share/Steam/steamapps/common/Proton - Experimental/proton"
+	IFS_BACKUP="$IFS"
+	IFS=":"
+	for COMPAT_PATH in $STEAM_COMPAT_TOOL_PATHS; do
+		if [ -f "$COMPAT_PATH/proton" ]; then
+			PROTON_BIN="$COMPAT_PATH/proton"
+		fi 
+	done
+	IFS="$IFS_BACKUP"
+	echo "Using proton binary: $PROTON_BIN"
+	exec "$PROTON_BIN" run "$STEAM_COMPAT_DATA_PATH/pfx/drive_c/Program Files/dotnet/dotnet.exe" Renderite.Host.dll "$@"
 }
 
 main "$@"
EOF
```
- Add the required DLLs to your launch options to load both RML and MelonLoader
```sh
WINEDLLOVERRIDES="version=n,b" %command% -LoadAssembly Libraries/ResoniteModLoader.dll
```

## Credits

Thanks to [AdamSki2003](https://git.adamski2003.lol/adam/ResoniteDESFix)
for providing the implementation of DES used on the FrooxEngine side,
as well as the skeleton of that mod.

Thanks to [Bredo](https://github.com/bredo228/Hardware.Info)
for providing the workaround for the hardware information gathering method
(in particular for counting CPU cores) causing a crash on Wine.

The rest of this rough mode set is brought to you by [Baplar](https://github.com/baplar).
