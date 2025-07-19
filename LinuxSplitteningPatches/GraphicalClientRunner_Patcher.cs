using HarmonyLib;

using GraphicalClient;
using System.Threading.Tasks;
using System.Threading;
using ResoniteModLoader;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(GraphicalClientRunner), "Run")]
public static class GraphicalClientRunner_Run_Patch {

    public static void Postfix(Task __result, Thread ___updateLoop) {
        if (___updateLoop != null) {
            __result = Task.Run(async () => {
                while (___updateLoop.IsAlive) {
                    await Task.Delay(1000);
                }
            });
        }
    }
}
