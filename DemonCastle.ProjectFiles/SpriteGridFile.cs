using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class SpriteGridFile {
		public string File { get; set; } = string.Empty;
		public int Width { get; set; } = 16;
		public int Height { get; set; } = 16;
		public int XSeparation { get; set; } = 0;
		public int YSeparation { get; set; } = 0;
		public int XOffset { get; set; } = 0;
		public int YOffset { get; set; } = 0;
		public List<SpriteData> Sprites { get; set; } = new List<SpriteData>();
	}

	public class SpriteData {
		public int X { get; set; }
		public int Y { get; set; }
		public string Name { get; set; } = string.Empty;
	}
}