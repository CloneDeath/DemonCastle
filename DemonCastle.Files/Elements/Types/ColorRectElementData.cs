using DemonCastle.Files.Common;
using Godot;

namespace DemonCastle.Files.Elements.Types;

[ElementType(ElementType.ColorRect)]
public class ColorRectElementData : ElementData {
	public ColorRectElementData() {
		Type = ElementType.ColorRect;
	}

	public ColorData Color { get; set; } = Colors.White.ToColorData();
}