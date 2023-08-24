using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game {
	public partial class GameLevel : TileMap {
		public Vector2 StartingLocation => Level.StartingLocation;

		protected void LoadLevel() {
			foreach (var area in Level.Areas) {
				LoadArea(area);
			}
		}

		protected void LoadArea(AreaInfo area) {
			foreach (var tileMapInfo in area.TileMap) {
				SetCell(0, tileMapInfo.Position, tileMapInfo.SourceId, tileMapInfo.AtlasCoords);
			}
		}
	}
}