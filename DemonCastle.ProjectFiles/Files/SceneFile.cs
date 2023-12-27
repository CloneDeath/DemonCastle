using System;
using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemonCastle.ProjectFiles.Files;

public class SceneFile {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public List<SceneElement> Elements { get; set; } = new();
}

public class SceneElement {
	public string Name { get; set; } = string.Empty;
	public Guid Id { get; set; } = Guid.NewGuid();
	public SceneElementType Type { get; set; }
	public Rect2I Region { get; set; }
	public Dictionary<string, string> StringProperties { get; set; } = new();
}

[JsonConverter(typeof(StringEnumConverter))]
public enum SceneElementType {
	Label
}