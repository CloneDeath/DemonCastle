using Godot;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class FrameOrigin {
	public Vector2I Anchor { get; set; } = Vector2I.Zero;
	public Vector2I Offset { get; set; } = Vector2I.Zero;
}