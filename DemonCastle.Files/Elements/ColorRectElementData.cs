using DemonCastle.Files.Common;
using Godot;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.ColorRect)]
public class ColorRectElementData : ElementData {
	public ColorRectElementData() {
		Name = "Color Rectangle";
		Type = ElementType.ColorRect;
		Region = new Region2I(0, 0, 16, 16);
	}

	public ColorData Color { get; set; } = Colors.White.ToColorData();
}