using System;
using Godot;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class WeaponFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
	public Vector2I Origin { get; set; } = Vector2I.Zero;
}