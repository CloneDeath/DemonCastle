using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Windows.Level.Area; 

public partial class AreaTileEditor : ScrollContainer {
	private void LoadArea(AreaInfo areaInfo) {
		foreach (var tileMapInfo in areaInfo.TileMap) {
			Root.AddChild(new TileCell(tileMapInfo));
		}
	}
}