using DemonCastle.ProjectFiles;

namespace DemonCastle.Projects.Data {
	public class LevelInfo : ResourceInfo<LevelFile>, IListableInfo {
		public LevelInfo(string filePath) : base(filePath) { }
		public string Name => Resource.Name;
		public int TileWidth => Resource.TileWidth;
	}
}