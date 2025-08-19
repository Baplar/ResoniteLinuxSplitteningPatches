using System.Collections.Generic;
using System.Threading.Tasks;

using Elements.Assets;

using FrooxEngine;

using SDL3;

namespace JankyLinuxClipboard;


public class SdlClipboardInterface : IClipboardInterface {
	public bool ContainsText => SDL.HasClipboardText();

	public bool ContainsFiles => false;

	public bool ContainsImage => false;

	public Task<string> GetText() {
		return Task.Run(SDL.GetClipboardText);
	}

	public Task<List<string>> GetFiles() {
		return Task.FromResult<List<string>>([]);
	}

	public Task<Bitmap2D> GetImage() {
		return Task.FromResult<Bitmap2D>(null);
	}

	public Task<bool> SetText(string text) {
		return Task.Run(() => SDL.ClearClipboardData() && SDL.SetClipboardText(text));
	}

	public Task<bool> SetBitmap(Bitmap2D bitmap) {
		return Task.FromResult(false);
	}

	public void Dispose() {}
}
