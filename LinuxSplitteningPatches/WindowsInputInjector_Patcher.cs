using HarmonyLib;
using FrooxEngine;
using WindowsInput;
using Elements.Core;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(WindowsInputInjector), MethodType.Constructor)]
public static class WindowsInputInjector_Constructor_Patcher {
    public static bool Prefix(ref WindowsInputInjector __instance) {
        if (Renderite.Shared.Helper.IsWine) {
            UniLog.Warning("Wine detected, skipping WindowsInputInjector initialization");
            AccessTools.Field(typeof(WindowsInputInjector), "_inputSimulator").SetValue(__instance, new InputSimulator());
            return false;
        }
        return true;
    }
}
