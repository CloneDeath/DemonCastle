using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;

namespace DemonCastle.Projects.Data {
	public class LevelInfo : IListableInfo {
		protected FileNavigator<LevelFile> File { get; }
		protected LevelFile Level => File.Resource;

		public LevelInfo(FileNavigator<LevelFile> file) {
			File = file;
		}
		
		public string Name => Level.Name;
		public int TileWidth => Level.TileWidth;
	}
}