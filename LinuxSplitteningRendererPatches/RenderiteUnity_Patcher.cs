using HarmonyLib;
using Renderite.Unity;

namespace LinuxSplitteningRendererPatches;

[HarmonyPatch(typeof(RenderingManager), "HasMainProcessExited", MethodType.Getter)]
public static class HasMainProcessExited_Patch {

    public static bool Prefix(ref RenderingManager __instance, ref bool __result)
    {
        __result = __instance.MainProcess.HasExited;
        return false;
    }
}
