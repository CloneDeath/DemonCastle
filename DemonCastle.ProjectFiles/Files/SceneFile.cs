using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files;

public class SceneFile {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public List<Element> Elements { get; set; } = new();
}

public class Element {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public ElementType Type { get; set; }
	public Rect2I Region { get; set; }
}

[JsonConverter(typeof(StringEnumConverter))]
public enum ElementType {
	Label,
	ColorRect
}

public class LabelElement : Element {
	public LabelElement() {
		Type = ElementType.Label;
	}

	public string Text { get; set; } = string.Empty;
	public Color Color { get; set; } = Colors.White;
}

public class ColorRectElement : Element {
	public ColorRectElement() {
		Type = ElementType.ColorRect;
	}

	public Color Color { get; set; } = Colors.White;
}