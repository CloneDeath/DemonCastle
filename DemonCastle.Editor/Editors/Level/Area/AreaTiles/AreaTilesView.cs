using System.ComponentModel;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class AreaTilesView : Control {
	private readonly AreaInfo _areaInfo;

	public AreaTilesView(AreaInfo areaInfo) {
		_areaInfo = areaInfo;
		ReloadArea();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_areaInfo.PropertyChanged += AreaInfo_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_areaInfo.PropertyChanged -= AreaInfo_OnPropertyChanged;
	}

	private void AreaInfo_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(_areaInfo.TileMap)) return;
		ReloadArea();
	}

	private void ReloadArea() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var tileMapInfo in _areaInfo.TileMap) {
			var tileView = new TileView(tileMapInfo);
			AddChild(tileView);
		}
	}
}