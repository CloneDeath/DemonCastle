using Godot;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class CharacterFrameWeaponData {
	public bool Enabled { get; set; }
	public Vector2I Position { get; set; } = Vector2I.Zero;
	public string Animation { get; set; } = string.Empty;
}