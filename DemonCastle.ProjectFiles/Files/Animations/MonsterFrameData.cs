using System;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class MonsterFrameData {
	public float Duration { get; set; } = 1;
	public Guid SpriteId { get; set; }
	public string Source { get; set; } = string.Empty;
}