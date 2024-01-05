using System;

namespace DemonCastle.Files.Elements.Types;

[ElementType(ElementType.Sprite)]
public class SpriteElementData : ElementData {
	public SpriteElementData() {
		Type = ElementType.Sprite;
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}