using System;
using System.Collections.Generic;

namespace DemonCastle.ProjectFiles.Files.Animations;

public class FrameData {
	public float Duration { get; set; } = 1;
	public string Source { get; set; } = string.Empty;
	public Guid SpriteId { get; set; }
	public FrameOrigin Origin { get; set; } = new();
	
	public List<FrameSlotData> Slots { get; set; } = new();
}