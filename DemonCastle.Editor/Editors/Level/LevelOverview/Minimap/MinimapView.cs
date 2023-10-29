using System;
using System.ComponentModel;
using DemonCastle.Editor.Editors.Components;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using Godot;

namespace DemonCastle.Editor.Editors.Level.LevelOverview.Minimap;

public partial class MinimapView : ScrollContainer {
	private readonly LevelInfo _levelInfo;
	protected Node2D Root;

	public event Action<AreaInfo>? AreaSelected;

	public MinimapView(LevelInfo levelInfo) {
		_levelInfo = levelInfo;

		Name = nameof(MinimapView);

		var control = new Control {
			Size = new Vector2(500, 500)
		};
		AddChild(control);

		control.AddChild(new GridControl {
			Size = new Vector2I(500, 500),
			GridSize = levelInfo.AreaSize
		});
		control.AddChild(Root = new Node2D());
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
		foreach (var child in Root.GetChildren()) {
			child.QueueFree();
		}
		foreach (var area in levelInfo.Areas) {
			var cell = new AreaCell(area);
			cell.Selected += Cell_OnSelected;
			Root.AddChild(cell);
		}
	}

	private void Cell_OnSelected(SelectableControl obj) {
		if (obj is not AreaCell cell) return;
		AreaSelected?.Invoke(cell.Area);
	}
}