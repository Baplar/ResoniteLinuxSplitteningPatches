using MelonLoader;

[assembly: MelonInfo(typeof(LinuxSplitteningRendererPatches.LinuxSplitteningRendererPatches), "LinuxSplitteningRendererPatches", "1.0.0", "Baplar", null)]
[assembly: MelonGame("Yellow Dog Man Studios", "Renderite.Renderer")]

namespace LinuxSplitteningRendererPatches
{
    public class LinuxSplitteningRendererPatches : MelonMod
    {
        public override void OnInitializeMelon()
        {
            HarmonyInstance.PatchAll();
            MelonLogger.Msg("Initialized.");
        }
    }
}