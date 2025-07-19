using System;
using System.Text;
using HarmonyLib;
using Elements.Core;
using ResoniteModLoader;
using Org.BouncyCastle.Utilities.Encoders;

namespace LinuxSplitteningPatches;

[HarmonyPatch(typeof(StringHelper))]
[HarmonyPatch(nameof(StringHelper.SimpleEncrypt))]
public static class EncryptPatch {
    public static bool Prefix(this string text, byte[] key, byte[] iv, ref string __result) {
        __result = Convert.ToBase64String(DESCrypto.Encrypt(text, key, iv));

        var keyStr = Encoding.UTF8.GetString(Hex.Encode(key));
        var ivStr = Encoding.UTF8.GetString(Hex.Encode(iv));

        ResoniteMod.Debug("SimpleEncrypt", text, keyStr, ivStr, __result);
        return false;
    }
}

[HarmonyPatch(typeof(StringHelper))]
[HarmonyPatch(nameof(StringHelper.SimpleDecrypt))]
public static class DecryptPatch {
    public static bool Prefix(this string text, byte[] key, byte[] iv, ref string __result) {

        __result = Encoding.Unicode.GetString(DESCrypto.Decrypt(text, key, iv));

        var keyStr = Encoding.UTF8.GetString(Hex.Encode(key));
        var ivStr = Encoding.UTF8.GetString(Hex.Encode(iv));

        ResoniteMod.Debug("SimpleDecrypt", text, keyStr, ivStr, __result, "");
        return false;
    }
}
