using Godot;

namespace DemonCastle.ProjectFiles.Files.Common;

public class ColorData {
	public int Red { get; set; }
	public int Green { get; set; }
	public int Blue { get; set; }

	public Color ToColor() {
		return Color.Color8((byte)Red, (byte)Green, (byte)Blue);
	}
}

public static class ColorExtensions {
	public static ColorData ToColorData(this Color color) {
		return new ColorData {
			Red = (byte)(color.R * 255),
			Green = (byte)(color.G * 255),
			Blue = (byte)(color.B * 255)
		};
	}
}