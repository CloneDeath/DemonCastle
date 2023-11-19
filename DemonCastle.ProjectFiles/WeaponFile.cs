using System;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.ProjectFiles;

public class WeaponFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<WeaponAnimationData> Animations { get; set; } = new();
}

public class WeaponAnimationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<WeaponFrameData> Frames { get; set; } = new();
}

public class WeaponFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
	public Vector2I Origin { get; set; } = Vector2I.Zero;
}