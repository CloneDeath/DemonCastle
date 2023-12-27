using System;
using System.Collections.Generic;
using DemonCastle.ProjectFiles.Files.Common;

namespace DemonCastle.ProjectFiles.Files;

public class SpriteAtlasFile {
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
}