using System.Diagnostics;
using System.IO;
using HarmonyLib;
using Renderite.Unity;

namespace LinuxSplitteningRendererPatches;

[HarmonyPatch(typeof(RenderingManager), "HasMainProcessExited", MethodType.Getter)]
public static class HasMainProcessExited_Patch {

    public static void Postfix(ref RenderingManager __instance, ref bool __result) {
        // If main process is actually managed, use it anyway
        if (Renderite.Shared.Helper.IsWine && __instance.MainProcess != null)
        {
            __result = __instance.MainProcess.HasExited;
        }
    }
}
