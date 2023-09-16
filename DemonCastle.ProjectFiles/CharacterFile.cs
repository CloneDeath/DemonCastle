using System.Collections.Generic;

namespace DemonCastle.ProjectFiles; 

public class CharacterFile {
	public string Name { get; set; } = string.Empty;
	public bool Enabled { get; set; } = true;
	public float WalkSpeed { get; set; } = 3;
	public float JumpHeight { get; set; } = 6;
	public float Gravity { get; set; } = 10;
	public float Height { get; set; } = 16;
	public float Width { get; set; } = 16;
	public List<AnimationData> Animations { get; set; } = new();
	public string WalkAnimation { get; set; } = string.Empty;
	public string IdleAnimation { get; set; } = string.Empty;
	public string JumpAnimation { get; set; } = string.Empty;
}

public class AnimationData {
	public string Name { get; set; } = string.Empty;
	public List<FrameData> Frames { get; set; } = new();
}