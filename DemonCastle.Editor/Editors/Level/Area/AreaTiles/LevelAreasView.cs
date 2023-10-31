using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class LevelAreasView : ControlView<ExpandingControl> {
	private readonly LevelInfo _levelInfo;

	public LevelAreasView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;
		Name = nameof(LevelAreasView);
		CellSize = levelInfo.TileSize;
		GridVisible = true;
		ReloadAreas();
	}

	private void ReloadAreas() {
		foreach (var child in MainControl.Inner.GetChildren()) {
			child.QueueFree();
		}

		foreach (var area in _levelInfo.Areas) {
			MainControl.Inner.AddChild(new AreaView(area) {
				MouseFilter = MouseFilterEnum.Pass
			});
		}
	}
}