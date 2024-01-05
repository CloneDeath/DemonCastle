using Godot;

namespace DemonCastle.Files.Common;

public class Region2I {
	public Region2I() : this(0, 0, 0, 0) {}
	public Region2I(int x, int y, int width, int height) {
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}

	public int X { get; set; }
	public int Y { get; set; }
	public int Width { get; set; }
	public int Height { get; set; }

	public Rect2I ToRect2I() => new(X, Y, Width, Height);
}

public static class Rect2IExtensions {
	public static Region2I ToRegion2I(this Rect2I self) {
		return new Region2I {
			X = self.Position.X,
			Y = self.Position.Y,
			Width = self.Size.X,
			Height = self.Size.Y
		};
	}
}