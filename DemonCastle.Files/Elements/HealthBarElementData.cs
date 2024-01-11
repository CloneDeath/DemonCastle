using System;
using DemonCastle.Files.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files.Elements;

[ElementType(ElementType.HealthBar)]
public class HealthBarElementData : ElementData {
	public HealthBarElementData() {
		Name = "Health Bar";
		Type = ElementType.HealthBar;
		Region = new Region2I(0, 0, 16, 16);
	}

	public string SpriteFile { get; set; } = string.Empty;
	public Guid SpriteId { get; set; } = Guid.Empty;
	public HealthBarSource Source = HealthBarSource.PlayerHP;
}

[JsonConverter(typeof(StringEnumConverter))]
public enum HealthBarSource {
	PlayerHP,
	PlayerMP,
	PlayerLives
}