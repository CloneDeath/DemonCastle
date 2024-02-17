using System;
using System.Collections.Generic;

namespace DemonCastle.Files;

public class SpriteGridFile : IGameFile {
	public int FileVersion => 1;
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
	public int X;
	public int Y;
	public string Name = string.Empty;
	public bool FlipHorizontal;
	public bool FlipVertical;
}