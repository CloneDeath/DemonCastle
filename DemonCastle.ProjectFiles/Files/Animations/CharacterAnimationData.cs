using System;
using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class CharacterAnimationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = string.Empty;
	public List<CharacterFrameData> Frames { get; set; } = new();
}