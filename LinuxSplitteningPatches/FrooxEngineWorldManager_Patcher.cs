using HarmonyLib;

using FrooxEngine;
using ResoniteModLoader;
using System.Collections.Generic;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(WorldManager), "Dispose")]
public static class FrooxEngineWorldManager_Dispose_Patch {

    public static bool Prefix(ref WorldManager __instance, ref List<string> ___worlds) {
        ResoniteMod.Msg("World Manager Dispose START");
        lock (__instance.Worlds) {
            foreach (World world in __instance.Worlds) {
                world.Dispose();
            }
            ___worlds.Clear();
        }
        return false;
    }
}
