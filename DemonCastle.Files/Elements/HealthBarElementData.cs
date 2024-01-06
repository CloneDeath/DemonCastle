using System;
using DemonCastle.Files.Common;

namespace DemonCastle.Files.Elements;

public class HealthBarElementData : ElementData {
	public HealthBarElementData() {
		Name = "Health Bar";
		Type = ElementType.HealthBar;
		Region = new Region2I(0, 0, 16, 16);
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
}