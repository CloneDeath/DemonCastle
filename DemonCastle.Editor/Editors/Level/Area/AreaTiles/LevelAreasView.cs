using System.Linq;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class LevelAreasView : ControlView<Container> {
	private readonly LevelInfo _levelInfo;

	public LevelAreasView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;
		CellSize = levelInfo.TileSize;
		MainControl_Grid.Color = new Color(Colors.White, 0.1f);
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

		var areas = _levelInfo.Areas.ToList();
		if (areas.Count <= 0) return;

		var minRect = GetRect2(areas[0]);
		foreach (var area in _levelInfo.Areas.Skip(1)) {
			var rect = GetRect2(area);
			minRect = minRect.Merge(rect);
		}

		MainControl.Inner.Size = minRect.Size;
		MainControl.Inner.CustomMinimumSize = minRect.Size;
	}

	protected static Rect2 GetRect2(AreaInfo area) {
		var position = area.PositionOfArea.ToLevelPositionInPixels();
		var size = area.SizeOfArea.ToPixelSize();
		return new Rect2(position, size);
	}
}