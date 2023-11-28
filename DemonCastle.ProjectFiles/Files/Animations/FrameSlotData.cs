using Godot;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class FrameSlotData {
	public string Name { get; set; } = string.Empty;
	public Vector2I Position { get; set; } = Vector2I.Zero;
	public string Animation { get; set; } = string.Empty;
}