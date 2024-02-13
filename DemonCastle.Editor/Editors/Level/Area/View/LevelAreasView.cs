using System;
using System.Collections.Generic;
using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.Editor.Icons;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View;

public partial class LevelAreasView : ControlView<ExpandingControl> {
	private readonly ProjectResources _resources;
	private readonly LevelInfo _levelInfo;
	private readonly Dictionary<AreaInfo, AreaView> _areaMap = new();
	private bool _showSingleLayer;
	private int _selectedLayerIndex;

	private Button ToggleLayersButton { get; }

	public event Action<AreaInfo>? AreaSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileSelected;
	public event Action<AreaInfo, Vector2I>? AreaTileCleared;

	public bool ShowSingleLayer {
		get => _showSingleLayer;
		set {
			_showSingleLayer = value;
			foreach (var areaView in _areaMap.Values) {
				areaView.ShowSingleLayer = value;
			}
		}
	}

	public int SelectedLayerIndex {
		get => _selectedLayerIndex;
		set {
			_selectedLayerIndex = value;
			foreach (var areaView in _areaMap.Values) {
				areaView.SelectedLayerIndex = value;
			}
		}
	}

	public LevelAreasView(ProjectResources resources, LevelInfo levelInfo) {
		_resources = resources;
		_levelInfo = levelInfo;
		Name = nameof(LevelAreasView);

		Toolbar.AddChild(ToggleLayersButton = new Button {
			Icon = IconTextures.AllLayersIcon,
			ToggleMode = true
		});
		ToggleLayersButton.Pressed += ToggleLayersButtonOnPressed;

		CellSize = levelInfo.TileSize;
		GridVisible = true;
		MainControl.Inner.MouseDefaultCursorShape = CursorShape.Arrow;
		ReloadAreas();
	}

	private void ToggleLayersButtonOnPressed() {
		ToggleLayersButton.Icon = ToggleLayersButton.ButtonPressed
			? IconTextures.SingleLayerIcon
			: IconTextures.AllLayersIcon;
		ShowSingleLayer = ToggleLayersButton.ButtonPressed;
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
			var areaView = new AreaView(_resources, area) {
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

		var region = area.Region.ToPixelRegionInLevel();
		if (!MainControl.GetVisibleRegion().Intersects(region)) {
			MainControl.CenterOnPosition(areaView.Position + areaView.Size / 2);
		}
	}

	public void DeselectAllAreas() {
		foreach (var areaView in _areaMap.Values) {
			areaView.IsSelected = false;
		}
	}
}