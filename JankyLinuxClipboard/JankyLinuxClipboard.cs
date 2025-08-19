using HarmonyLib;
using ResoniteModLoader;

namespace JankyLinuxClipboard;
//More info on creating mods can be found https://github.com/resonite-modding-group/ResoniteModLoader/wiki/Creating-Mods
public class JankyLinuxClipboard : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0"; //Changing the version here updates it in all locations needed
	public override string Name => "JankyLinuxClipboard";
	public override string Author => "Baplar";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/Baplar/ResoniteLinuxSplitteningPatches/";

	public override void OnEngineInit() {
		Harmony harmony = new Harmony("fr.baplar.JankyLinuxClipboard");
		harmony.PatchAll();
	}
}
