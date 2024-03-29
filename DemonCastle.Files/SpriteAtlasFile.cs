using System;
using System.Collections.Generic;
using DemonCastle.Files.Common;

namespace DemonCastle.Files;

public class SpriteAtlasFile : IGameFile {
	public int FileVersion => 1;
	public string File { get; set; } = string.Empty;
	public ColorData TransparentColor { get; set; } = new() {
		Red = 255,
		Green = 0,
		Blue = 255
	};
	public List<SpriteAtlasData> Sprites { get; set; } = new();
}

public class SpriteAtlasData {
	public Guid Id { get; set; } = Guid.NewGuid();
	public string Name = string.Empty;
	public int X;
	public int Y;
	public int Width;
	public int Height;
	public bool FlipHorizontal;
	public bool FlipVertical;
}