using Elements.Core;

using FrooxEngine;

using ResoniteModLoader;

using SDL3;

namespace JankyLinuxClipboard;
//More info on creating mods can be found https://github.com/resonite-modding-group/ResoniteModLoader/wiki/Creating-Mods
public class JankyLinuxClipboard : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0"; //Changing the version here updates it in all locations needed
	public override string Name => "JankyLinuxClipboard";
	public override string Author => "Baplar";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/Baplar/ResoniteLinuxSplitteningPatches/";

	public override void OnEngineInit() {
		Engine.Current.RunPostInit(SetupSdlClipboard);
	}

	public static void SetupSdlClipboard() {
		if (Engine.Current.InputInterface.IsClipboardSupported || Engine.Current.Platform != Platform.Linux) {
			return;
		}

		UniLog.Log("Registering SDL clipboard interface");

		if (!SDL.SetHint(SDL.Hints.VideoDriver, "x11")) {
			UniLog.Warning("Could not enforce SDL video driver to x11");
		}

		if (!SDL.InitSubSystem(SDL.InitFlags.Video)) {
			UniLog.Warning($"Could not initialize SDL video subsystem: {SDL.GetError()}");
		} else {
			UniLog.Log($"SDL video subsystem initialized with driver {SDL.GetCurrentVideoDriver()}");
			Engine.Current.InputInterface.RegisterClipboardInterface(new SdlClipboardInterface());
		}
    }
}
