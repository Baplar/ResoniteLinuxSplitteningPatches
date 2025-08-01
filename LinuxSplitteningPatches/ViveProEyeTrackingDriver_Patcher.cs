using HarmonyLib;
using FrooxEngine;
using Elements.Core;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(ViveProEyeTrackingDriver), "SupressRegistration")]
public static class ViveProEyeTrackingDriver_SupressRegistration_Patcher {
    public static void Postfix(ref bool __result) {
        if (!__result && Engine.Current.Platform == Platform.Linux) {
            UniLog.Log("Running on Linux, supressing SRAnipal");
            __result = true;
        }
    }
}
