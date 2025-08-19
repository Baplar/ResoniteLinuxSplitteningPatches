using HarmonyLib;
using Elements.Core;
using FrooxEngine;
using SDL3;

namespace JankyLinuxClipboard;

[HarmonyPatch(typeof(Engine), "FinishInitialization")]
public static class FrooxEngine_FinishInitialization_Patch {
    public static void Postfix(ref Engine __instance) {
        if (__instance.Platform == Platform.Linux && !__instance.InputInterface.IsClipboardSupported) {
            UniLog.Log("Registering SDL clipboard interface");
            if (!SDL.SetHint(SDL.Hints.VideoDriver, "x11")) {
                UniLog.Warning("Could not enforce SDL video driver to x11");
            }
            if (!SDL.InitSubSystem(SDL.InitFlags.Video)) {
                UniLog.Warning($"Could not initialize SDL video subsystem: {SDL.GetError()}");
            } else {
                UniLog.Log($"SDL video subsystem initialized with driver {SDL.GetCurrentVideoDriver()}");
                __instance.InputInterface.RegisterClipboardInterface(new SdlClipboardInterface());
            }
        }
    }
}