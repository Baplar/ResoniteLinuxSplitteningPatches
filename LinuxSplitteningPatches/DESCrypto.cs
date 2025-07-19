using System;

using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

namespace LinuxSplitteningPatches;


public class DESCrypto {
	public static byte[] Encrypt(string text, byte[] key, byte[] iv) {
		var cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEngine()), new Pkcs7Padding());
		cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));

		byte[] bytes = System.Text.Encoding.Unicode.GetBytes(text);

		return cipher.DoFinal(bytes, 0, bytes.Length);
	}

	public static byte[] Decrypt(string text, byte[] key, byte[] iv) {
		var cipher = new PaddedBufferedBlockCipher(new CbcBlockCipher(new DesEngine()), new Pkcs7Padding());
		cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));

		byte[] bytes = Convert.FromBase64String(text);

		return cipher.DoFinal(bytes, 0, bytes.Length);
	}
}
