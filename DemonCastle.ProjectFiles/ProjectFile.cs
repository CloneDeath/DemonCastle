using System.Collections.Generic;

namespace DemonCastle.ProjectFiles {
	public class ProjectFile {
		public string Name { get; set; } = string.Empty;
		public int UnitWidth { get; set; } = 16;
		public int UnitHeight { get; set; } = 16;
		public List<string> Characters { get; set; } = new List<string>();
		public List<string> Levels { get; set; } = new List<string>();
	}
}