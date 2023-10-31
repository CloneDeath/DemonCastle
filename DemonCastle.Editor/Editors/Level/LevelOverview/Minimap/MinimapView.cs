using System;
using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.Editor.Editors.Components.ControlViewComponent;
using DemonCastle.ProjectFiles.Projects.Data.Levels;

namespace DemonCastle.Editor.Editors.Level.LevelOverview.Minimap;

public partial class MinimapView : ControlView<ExpandingControl> {
	private readonly LevelInfo _levelInfo;
	public event Action<AreaInfo>? AreaSelected;

	public MinimapView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;

		Name = nameof(MinimapView);
		CellSize = levelInfo.AreaSize;
		GridVisible = true;
		LoadLevel(levelInfo);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_levelInfo.PropertyChanged += LevelInfoOnPropertyChanged;
		Reload();
	}

	public override void _ExitTree() {
		base._ExitTree();
		_levelInfo.PropertyChanged -= LevelInfoOnPropertyChanged;
	}

	private void LevelInfoOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		Reload();
	}

	public void Reload() => LoadLevel(_levelInfo);

	private void LoadLevel(LevelInfo levelInfo) {
		foreach (var child in MainControl.Inner.GetChildren()) {
			child.QueueFree();
		}
		foreach (var area in levelInfo.Areas) {
			var cell = new AreaCell(area);
			cell.Selected += Cell_OnSelected;
			MainControl.Inner.AddChild(cell);
		}
	}

	private void Cell_OnSelected(SelectableControl obj) {
		if (obj is not AreaCell cell) return;
		AreaSelected?.Invoke(cell.Area);
	}
}