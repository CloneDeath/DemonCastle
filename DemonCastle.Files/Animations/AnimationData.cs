using System;
using System.Collections.Generic;

namespace DemonCastle.Files.Animations;

public class AnimationData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name { get; set; } = "animation";
	public List<FrameData> Frames { get; set; } = new();
}