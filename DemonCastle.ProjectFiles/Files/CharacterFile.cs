using System;
using System.Collections.Generic;
using Godot;

namespace DemonCastle.ProjectFiles.Files;

public class CharacterFile {
	public string Name { get; set; } = string.Empty;
	public bool Enabled { get; set; } = true;
	public float WalkSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 6;
	public float Gravity { get; set; } = 10;
	public float Height { get; set; } = 16;
	public float Width { get; set; } = 16;
	public string DefaultWeapon { get; set; } = string.Empty;
	public List<CharacterAnimationData> Animations { get; set; } = new();
	public Guid WalkAnimation { get; set; } = Guid.Empty;
	public Guid IdleAnimation { get; set; } = Guid.Empty;
	public Guid JumpAnimation { get; set; } = Guid.Empty;
	public Guid CrouchAnimation { get; set; } = Guid.Empty;
	public Guid StairsUpAnimation { get; set; } = Guid.Empty;
	public Guid StairsDownAnimation { get; set; } = Guid.Empty;
	public Guid StandAttackAnimation { get; set; } = Guid.Empty;
	public Guid JumpAttackAnimation { get; set; } = Guid.Empty;
	public Guid StairsUpAttackAnimation { get; set; } = Guid.Empty;
	public Guid StairsDownAttackAnimation { get; set; } = Guid.Empty;
}

public class CharacterAnimationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<CharacterFrameData> Frames { get; set; } = new();
}

public class CharacterFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
	public CharacterFrameWeaponData Weapon { get; set; } = new();
}

public class CharacterFrameWeaponData {
	public bool Enabled { get; set; }
	public Vector2I Position { get; set; } = Vector2I.Zero;
	public string Animation { get; set; } = string.Empty;
}