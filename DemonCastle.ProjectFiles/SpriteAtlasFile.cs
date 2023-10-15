using System.Collections.Generic;
using Godot;

namespace DemonCastle.ProjectFiles;

public class SpriteAtlasFile {
	public string File { get; set; } = string.Empty;
	public ColorData TransparentColor { get; set; } = new();
	public List<SpriteAtlasData> Sprites { get; set; } = new();
}

public class ColorData {
	public int Red { get; set; }
	public int Green { get; set; }
	public int Blue { get; set; }

	public Color ToColor() {
		return Color.Color8((byte)Red, (byte)Green, (byte)Blue);
	}
}

public class SpriteAtlasData {
	public string Name = string.Empty;
	public int X;
	public int Y;
	public int Width;
	public int Height;
	public bool FlipHorizontal;
}