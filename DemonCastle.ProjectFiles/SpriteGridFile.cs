using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public partial class SpriteGridFile {
		public string File { get; set; } = string.Empty;
		public int Width { get; set; } = 16;
		public int Height { get; set; } = 16;
		public int XSeparation { get; set; }
		public int YSeparation { get; set; }
		public int XOffset { get; set; }
		public int YOffset { get; set; }
		public List<SpriteGridData> Sprites { get; set; } = new List<SpriteGridData>();
	}

	public partial class SpriteGridData {
		public int X { get; set; }
		public int Y { get; set; }
		public string Name { get; set; } = string.Empty;
		public bool FlipHorizontal { get; set; }
	}
}