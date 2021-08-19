using DemonCastle.ProjectFiles;
using DemonCastle.Projects.Resources;
using Godot;

namespace DemonCastle.Projects.Data {
	public class TileInfo {
		public int Index { get; }
		protected TileData TileData { get; }
		protected FileNavigator<LevelFile> Level { get; }

		public TileInfo(FileNavigator<LevelFile> level, TileData tileData, int index) {
			Index = index;
			TileData = tileData;
			Level = level;
		}
		
		public string Name => TileData.Name;
		protected ISpriteInfo Sprite => Level.GetSprite(TileData.Source);
		public Texture Texture => Sprite.Texture;
		public Rect2 Region => Sprite.GetRegion(TileData.Sprite);
	}
}