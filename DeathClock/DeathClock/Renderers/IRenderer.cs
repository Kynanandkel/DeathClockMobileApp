using System;
using SkiaSharp;

namespace DeathClock.Renderers
{
	public interface IRenderer
	{
		void PaintSurface(SKSurface surface, SKImageInfo info);

		event EventHandler RefreshRequested;
	}
}

