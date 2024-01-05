using System;

namespace DemonCastle.Files.Elements;

public class HealthBarElementData : ElementData {
	public HealthBarElementData() {
		Type = ElementType.HealthBar;
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}