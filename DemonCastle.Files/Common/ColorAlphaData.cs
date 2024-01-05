using Godot;

namespace DemonCastle.Files.Common;

public class ColorAlphaData {
	public int Red { get; set; }
	public int Green { get; set; }
	public int Blue { get; set; }
	public int Alpha { get; set; }

	public Color ToColor() {
		return Color.Color8((byte)Red, (byte)Green, (byte)Blue, (byte)Alpha);
	}
}

public static class ColorAlphaExtensions {
	public static ColorAlphaData ToColorAlphaData(this Color color) {
		return new ColorAlphaData {
			Red = (byte)(color.R * 255),
			Green = (byte)(color.G * 255),
			Blue = (byte)(color.B * 255),
			Alpha = (byte)(color.A * 255)
		};
	}
}