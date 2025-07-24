using HarmonyLib;
using Renderite.Unity;
using UnityEngine;

namespace LinuxSplitteningRendererPatches;

[HarmonyPatch(typeof(RenderingManager), "HasMainProcessExited", MethodType.Getter)]
public static class HasMainProcessExited_Patch
{
    private static bool hasSearchedWineProcess = false;

    public static void Postfix(ref RenderingManager __instance, ref bool __result, int ____mainProcessId)
    {
        if (!hasSearchedWineProcess)
        {
            if (__instance.MainProcess == null)
            {
                Debug.LogWarning($"MainProcess was null, searching id {____mainProcessId} in case main process is on Wine too");
                try
                {
                    AccessTools.PropertySetter(typeof(RenderingManager), "MainProcess").Invoke(__instance, [System.Diagnostics.Process.GetProcessById(____mainProcessId)]);
                    Debug.Log("MainProcess was found, we are probably on a full Wine setup");
                }
                catch
                {
                    Debug.Log("Could not find process by ID, we are on a hybrid Native/Wine setup");
                }
            }
            hasSearchedWineProcess = true;
        }
        if (Renderite.Shared.Helper.IsWine && __instance.MainProcess != null)
        {
            __result &= __instance.MainProcess.HasExited;
        }
    }
}
