using System;

namespace DemonCastle.ProjectFiles.Files.Elements.Types;

public class SpriteElementData : ElementData {
	public SpriteElementData() {
		Type = ElementType.Sprite;
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}