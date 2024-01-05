using Godot;

namespace DemonCastle.Files.Common;

public class Size {
	public Size() : this(16, 16) {}

	public Size(int width, int height) {
		Width = width;
		Height = height;
	}

	public int Width { get; set; }
	public int Height { get; set; }

	public Vector2I ToVector2I() => new(Width, Height);
}

public static class Vector2ISizeExtensions {
	public static Size ToSize(this Vector2I vector2I) {
		return new Size(vector2I.X, vector2I.Y);
	}
}