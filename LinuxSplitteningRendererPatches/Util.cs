using System.Runtime.InteropServices;

namespace LinuxSplitteningRendererPatches;

internal static class Util {
    static Util() {
        try {
            GetWineVersion();
            IsWine = true;
        } catch {
            // Leave at false
        }
    }

    [DllImport("ntdll.dll", EntryPoint = "wine_get_version")]
    private static extern string GetWineVersion();

    internal static readonly bool IsWine;
}
