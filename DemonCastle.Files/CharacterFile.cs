using System;
using System.Collections.Generic;
using DemonCastle.Files.Animations;

namespace DemonCastle.Files;

public class CharacterFile : IGameFile {
	public int FileVersion => 1;

	public string Name { get; set; } = string.Empty;
	public bool Enabled { get; set; } = true;
	public float WalkSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 6;
	public float Gravity { get; set; } = 10;
	public float Height { get; set; } = 16;
	public float Width { get; set; } = 16;
	public string DefaultWeapon { get; set; } = string.Empty;
	public List<AnimationData> Animations { get; set; } = new();
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