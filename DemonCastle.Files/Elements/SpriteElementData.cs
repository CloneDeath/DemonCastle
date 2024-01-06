using System;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.Sprite)]
public class SpriteElementData : ElementData {
	public SpriteElementData() {
		Name = "Sprite";
		Type = ElementType.Sprite;
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}