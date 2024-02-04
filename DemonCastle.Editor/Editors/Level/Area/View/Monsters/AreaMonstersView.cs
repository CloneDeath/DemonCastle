using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Monsters;

public partial class AreaMonstersView : Container {
	private readonly ProjectInfo _project;
	private readonly AreaInfo _area;

	public AreaMonstersView(ProjectInfo project, AreaInfo area) {
		_project = project;
		_area = area;
		Reload();
	}

	public override void _EnterTree() {
		base._EnterTree();
		_area.Monsters.CollectionChanged += Monsters_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_area.Monsters.CollectionChanged -= Monsters_OnCollectionChanged;
	}

	private void Monsters_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		foreach (var monster in _area.Monsters) {
			AddChild(new MonsterDataView(_project, monster));
		}
	}
}