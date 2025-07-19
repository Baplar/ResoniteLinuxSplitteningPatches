using HarmonyLib;
using MelonLoader;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace LinuxSplitteningRendererPatches;

[HarmonyPatch("Cloudtoid.Interprocess.InterprocessSemaphore", "CreateWaiter")]
public static class CreateWaiter_Patch
{

    public static void Postfix(string name, ref object __result)
    {
        Melon<LinuxSplitteningRendererPatches>.Logger.Warning("CreateWaiter_Patch");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Util.IsWine)
        {
            Type SemaphoreLinux = AccessTools.TypeByName("Cloudtoid.Interprocess.Semaphore.Linux.SemaphoreLinux");
            __result = SemaphoreLinux.GetConstructor([typeof(string), typeof(bool)]).Invoke([name]);
        }
    }
}

[HarmonyPatch("Cloudtoid.Interprocess.InterprocessSemaphore", "CreateReleaser")]
public static class CreateReleaser_Patch
{

    public static void Postfix(string name, ref object __result)
    {
        Melon<LinuxSplitteningRendererPatches>.Logger.Warning("CreateReleaser_Patch");
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Util.IsWine)
        {
            Type SemaphoreLinux = AccessTools.TypeByName("Cloudtoid.Interprocess.Semaphore.Linux.SemaphoreLinux");
            __result = SemaphoreLinux.GetConstructor([typeof(string), typeof(bool)]).Invoke([name]);
        }
    }
}

[HarmonyPatch("Cloudtoid.Interprocess.Subscriber", "DequeueCore")]
public static class Subscriber_DequeueCore_Patch
{

    public static bool Prefix(
        Memory<byte>? resultBuffer,
        CancellationToken cancellation,
        ref object __instance,
        ref ReadOnlyMemory<byte> __result,
        CancellationTokenSource ___cancellationSource,
        CountdownEvent ___countdownEvent,
        ref object ___signal
    )
    {
        if (!semaphoreReplaced)
        {
            lock (___signal)
            {
                string name = AccessTools.Field(___signal.GetType(), "name").GetValue(___signal) as string;
                string prefix = AccessTools.Field(___signal.GetType(), "HandleNamePrefix").GetValue(null) as string;
                name = name.Replace(prefix, "");
                AccessTools.Method(___signal.GetType(), "Dispose").Invoke(___signal, []);
                Type InterprocessSemaphore = AccessTools.TypeByName("Cloudtoid.Interprocess.InterprocessSemaphore");
                ___signal = AccessTools.Method(InterprocessSemaphore, "CreateWaiter").Invoke(null, [name]);
            }
            semaphoreReplaced = true;
        }

        if (___cancellationSource.IsCancellationRequested)
        {
            throw new OperationCanceledException();
        }
        cancellation.ThrowIfCancellationRequested();
        ___countdownEvent.AddCount();
        try
        {
            Type Semaphore = AccessTools.TypeByName("Cloudtoid.Interprocess.IInterprocessSemaphoreWaiter");
            MethodInfo Wait = AccessTools.Method(Semaphore, "Wait");

            Type Subscriber = AccessTools.TypeByName("Cloudtoid.Interprocess.Subscriber");
            MethodInfo TryDequeueImpl = AccessTools.Method(Subscriber, "TryDequeueImpl");
            object[] parameters = [resultBuffer, cancellation, null];

            while (!(TryDequeueImpl.Invoke(__instance, parameters) as bool?).GetValueOrDefault(false))
            {
                if (Wait.Invoke(___signal, [1]) as bool? == true) {
                    Melon<LinuxSplitteningRendererPatches>.Logger.Msg("The semaphore did a thing");
                }
            }
            if (parameters[2] != null && parameters[2] is ReadOnlyMemory<byte> memory)
            {
                __result = memory;
            }
        }
        finally
        {
            ___countdownEvent.Signal();
        }
        return false;
    }

    private static bool semaphoreReplaced;
}

[HarmonyPatch("Cloudtoid.Interprocess.Publisher", "TryEnqueue")]
public static class Publisher_TryEnqueue_Patch {

    public static bool Prefix(
        ref object ___signal
    ) {
        if (!semaphoreReplaced) {
            lock (___signal) {
                string name = AccessTools.Field(___signal.GetType(), "name").GetValue(___signal) as string;
                string prefix = AccessTools.Field(___signal.GetType(), "HandleNamePrefix").GetValue(null) as string;
                name = name.Replace(prefix, "");
                AccessTools.Method(___signal.GetType(), "Dispose").Invoke(___signal, []);
                Type InterprocessSemaphore = AccessTools.TypeByName("Cloudtoid.Interprocess.InterprocessSemaphore");
                ___signal = AccessTools.Method(InterprocessSemaphore, "CreateReleaser").Invoke(null, [name]);
            }
            semaphoreReplaced = true;
        }
        return true;
    }

    private static bool semaphoreReplaced;
}
