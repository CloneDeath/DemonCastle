using System;
using System.Collections.Generic;

namespace DemonCastle.Files;

public class SpriteGridFile {
	public string File { get; set; } = string.Empty;
	public int Width { get; set; } = 16;
	public int Height { get; set; } = 16;
	public int XSeparation { get; set; }
	public int YSeparation { get; set; }
	public int XOffset { get; set; }
	public int YOffset { get; set; }
	public List<SpriteGridData> Sprites { get; set; } = new();
}

public class SpriteGridData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public int X { get; set; }
	public int Y { get; set; }
	public string Name { get; set; } = string.Empty;
	public bool FlipHorizontal { get; set; }
}