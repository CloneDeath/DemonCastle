using System;

namespace DemonCastle.ProjectFiles.Files.Elements.Types;

public class HealthBarElementData : ElementData {
	public HealthBarElementData() {
		Type = ElementType.HealthBar;
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}