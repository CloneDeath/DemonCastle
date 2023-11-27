using System;
using Godot;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class FrameData {
	public float Duration { get; set; } = 1;
	public string Source { get; set; } = string.Empty;
	public Guid SpriteId { get; set; }
	public FrameOrigin Origin { get; set; } = new();
}

public class FrameOrigin {
	public Vector2I Anchor { get; set; } = Vector2I.Zero;
	public Vector2I Offset { get; set; } = Vector2I.Zero;
}