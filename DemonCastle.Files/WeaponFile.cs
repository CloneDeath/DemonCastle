using System;
using System.Collections.Generic;
using DemonCastle.Files.Animations;

namespace DemonCastle.Files;

public class WeaponFile : IGameFile {
	public int FileVersion => 1;
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<AnimationData> Animations { get; set; } = new();
}