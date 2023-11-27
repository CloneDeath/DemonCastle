using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Animations;

namespace DemonCastle.ProjectFiles.Files;

public class WeaponFile {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<AnimationData> Animations { get; set; } = new();
}