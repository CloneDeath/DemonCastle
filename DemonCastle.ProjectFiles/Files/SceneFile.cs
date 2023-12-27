using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Common;
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
	public Region2I Region { get; set; } = new();
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
	public ColorData Color { get; set; } = Colors.White.ToColorData();
}

public class ColorRectElement : Element {
	public ColorRectElement() {
		Type = ElementType.ColorRect;
	}

	public ColorData Color { get; set; } = Colors.White.ToColorData();
}