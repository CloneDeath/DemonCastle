using System;
using System.Collections.Generic;
using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.AreaTiles;

public partial class LevelAreasView : ControlView<ExpandingControl> {
	private readonly LevelInfo _levelInfo;
	private readonly Dictionary<AreaInfo, AreaView> _areaMap = new();

	public event Action<AreaInfo>? AreaSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileCleared;

	public LevelAreasView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;
		Name = nameof(LevelAreasView);
		CellSize = levelInfo.TileSize;
		GridVisible = true;
		MainControl.Inner.MouseDefaultCursorShape = CursorShape.Arrow;
		ReloadAreas();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_levelInfo.PropertyChanged += LevelInfo_OnPropertyChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_levelInfo.PropertyChanged -= LevelInfo_OnPropertyChanged;
	}

	private void LevelInfo_OnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		if (e.PropertyName != nameof(_levelInfo.Areas)) return;
		ReloadAreas();
	}

	private void ReloadAreas() {
		foreach (var child in MainControl.Inner.GetChildren()) {
			child.QueueFree();
		}
		_areaMap.Clear();

		foreach (var area in _levelInfo.Areas) {
			var areaView = new AreaView(area) {
				MouseFilter = MouseFilterEnum.Pass
			};
			areaView.Selected += _ => AreaSelected?.Invoke(area);
			areaView.AreaTileSelected += (areaInfo, index) => AreaTileSelected?.Invoke(areaInfo, index);
			areaView.AreaTileCleared += (areaInfo, index) => AreaTileCleared?.Invoke(areaInfo, index);
			MainControl.Inner.AddChild(areaView);
			_areaMap[area] = areaView;
		}
	}

	public void SelectArea(AreaInfo area) {
		if (!_areaMap.ContainsKey(area)) return;
		var areaView = _areaMap[area];
		areaView.IsSelected = true;
		MainControl.CenterOnPosition(areaView.Position + areaView.Size / 2);
	}

	public void DeselectAllAreas() {
		foreach (var areaView in _areaMap.Values) {
			areaView.IsSelected = false;
		}
	}
}