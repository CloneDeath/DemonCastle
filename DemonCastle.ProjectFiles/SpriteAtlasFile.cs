using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class SpriteAtlasFile {
		public string File { get; set; } = string.Empty;
		public ColorData TransparentColor { get; set; } = new();
		public List<SpriteAtlasData> Sprites { get; set; } = new();
	}

	public class ColorData {
		public int Red { get; set; }
		public int Green { get; set; }
		public int Blue { get; set; }
	}

	public class SpriteAtlasData {
		public string Name { get; set; } = string.Empty;
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public bool FlipHorizontal { get; set; }
	}
}