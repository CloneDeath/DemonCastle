using DemonCastle.ProjectFiles.Files.Common;
using Godot;

namespace DemonCastle.ProjectFiles.Files.Elements.Types;

public class ColorRectElementData : ElementData {
	public ColorRectElementData() {
		Type = ElementType.ColorRect;
	}

	public ColorData Color { get; set; } = Colors.White.ToColorData();
}