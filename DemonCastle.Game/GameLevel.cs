using System.Linq;
using DemonCastle.Projects.Data;
using DemonCastle.Projects.Data.Levels;
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
				SetCellv(tileMapInfo.Position, tileMapInfo.TileIndex);
			}
		}
	}
}