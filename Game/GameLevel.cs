using DemonCastle.Projects.Data;
using DemonCastle.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Game {
	public partial class GameLevel : TileMap {
		protected void LoadLevel() {
			foreach (var area in Level.Areas) {
				LoadArea(area);
			}
		}

		protected void LoadArea(AreaInfo area) {
			foreach (var tileMapInfo in area.TileMap) {
				SetCellv(tileMapInfo.Position, tileMapInfo.TileIndex);
			}
		}
	}
}