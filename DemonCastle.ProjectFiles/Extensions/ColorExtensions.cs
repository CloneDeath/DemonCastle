using Godot;

namespace DemonCastle.ProjectFiles.Extensions; 

public static class ColorExtensions {
	public static ColorData ToColorData(this Color color) {
		return new ColorData {
			Red = (byte)(color.R * 255),
			Green = (byte)(color.G * 255),
			Blue = (byte)(color.B * 255),
		};
	}
}