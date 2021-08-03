using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class CharacterFile {
		public string Name { get; set; } = string.Empty;
		public float WalkSpeed { get; set; } = 3;
		public float JumpHeight { get; set; } = 6;
		public List<AnimationInfo> Animations { get; set; } = new List<AnimationInfo>();
		public string WalkAnimation { get; set; } = string.Empty;
		public string IdleAnimation { get; set; } = string.Empty;
		public string JumpAnimation { get; set; } = string.Empty;
	}

	public class AnimationInfo {
		public string Name { get; set; } = string.Empty;
		public List<FrameInfo> Frames { get; set; } = new List<FrameInfo>();
	}
}