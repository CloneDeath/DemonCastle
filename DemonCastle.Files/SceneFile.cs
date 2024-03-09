using System;
using System.Collections.Generic;
using DemonCastle.Files.Common;
using DemonCastle.Files.Events;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.Files;

public class SceneFile : IGameFile {
	public int FileVersion => 1;
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public Size Size { get; set; } = new(256, 240);
	public ColorAlphaData BackgroundColor { get; set; } = Colors.Black.ToColorAlphaData();
	public List<ElementData> Elements { get; set; } = new();
	public List<SceneEventData> Events { get; set; } = new();
}

public class ElementData {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public ElementType Type { get; set; }
	public Region2I Region { get; set; } = new();
}

[JsonConverter(typeof(StringEnumConverter))]
public enum ElementType {
	ColorRect,
	HealthBar,
	Label,
	LevelView,
	Sprite,
	OptionList
}

[AttributeUsage(AttributeTargets.Class)]
public class ElementTypeAttribute : Attribute {
	public ElementType ElementType { get; }

	public ElementTypeAttribute(ElementType elementType) {
		ElementType = elementType;
	}
}