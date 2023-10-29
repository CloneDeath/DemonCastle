using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class LevelAreasView : ScrollContainer {
	protected Container AreaContainer { get; }
	private readonly LevelInfo _levelInfo;

	public LevelAreasView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;
		AddChild(AreaContainer = new Container());

		ReloadAreas();
	}

	private void ReloadAreas() {
		foreach (var child in AreaContainer.GetChildren()) {
			child.QueueFree();
		}

		foreach (var area in _levelInfo.Areas) {
			AreaContainer.AddChild(new AreaView(area));
		}
	}

	public override void _Process(double delta) {
		base._Process(delta);

		var areas = _levelInfo.Areas.ToList();
		if (areas.Count <= 0) return;

		var minRect = GetRect2(areas[0]);
		foreach (var area in _levelInfo.Areas.Skip(1)) {
			var rect = GetRect2(area);
			minRect = minRect.Merge(rect);
		}

		AreaContainer.Position = minRect.Position;
		AreaContainer.Size = minRect.Size;
		AreaContainer.CustomMinimumSize = minRect.Size;
	}

	protected static Rect2 GetRect2(AreaInfo area) {
		var position = area.PositionOfArea.ToLevelPositionInPixels();
		var size = area.SizeOfArea.ToPixelSize();
		return new Rect2(position, size);
	}
}