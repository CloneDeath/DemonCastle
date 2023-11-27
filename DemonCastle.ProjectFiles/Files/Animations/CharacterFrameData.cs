using System;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class CharacterFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
	public FrameOrigin Origin { get; set; } = new();

	public CharacterFrameWeaponData Weapon { get; set; } = new();
}