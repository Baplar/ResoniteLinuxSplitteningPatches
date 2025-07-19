using HarmonyLib;

using FrooxEngine;
using ResoniteModLoader;
using System.IO;
using System.Threading;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(Engine), "Dispose")]
public static class FrooxEngineEngine_Dispose_Patch {

    public static bool Prefix(
        ref Engine __instance,
        CancellationTokenSource ____globalCancellationToken,
        FileSystemWatcher ____localeChangeWatcher,
        FileStream ____appInstanceLock
    ) {
        ResoniteMod.Msg("Engine Dispose START");
        ResoniteMod.Msg("Global cancellation token Cancel");
        ____globalCancellationToken.Cancel();
        ResoniteMod.Msg("World Manager Dispose");
        __instance.WorldManager.Dispose();
        ResoniteMod.Msg("Local DB Dispose");
        __instance.LocalDB.Dispose();
        ResoniteMod.Msg("Asset Manager Dispose");
        __instance.AssetManager.Dispose();
        ResoniteMod.Msg("Work Processer Dispose");
        __instance.WorkProcessor.Dispose();
        ResoniteMod.Msg("Session Announcer Dispose");
        AccessTools.Method(typeof(SessionAnnouncer), "Dispose").Invoke(__instance.SessionAnnouncer, null);
        ResoniteMod.Msg("Input Interface Dispose");
        __instance.InputInterface.Dispose();
        ResoniteMod.Msg("Platform Interface Dispose");
        __instance.PlatformInterface.Dispose();
        ResoniteMod.Msg("Locale Change Watcher Dispose");
        ____localeChangeWatcher?.Dispose();
        ResoniteMod.Msg("App Instance Lock Dispose");
        ____appInstanceLock.Dispose();
        return false;
    }
}
