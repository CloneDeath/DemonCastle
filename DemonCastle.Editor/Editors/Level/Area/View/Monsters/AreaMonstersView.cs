using System.Collections.Specialized;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Areas;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.View.Monsters;

public partial class AreaMonstersView : Container {
	private readonly ProjectResources _resources;
	private readonly AreaInfo _area;

	public AreaMonstersView(ProjectResources resources, AreaInfo area) {
		Name = nameof(AreaMonstersView);

		_resources = resources;
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
			AddChild(new MonsterDataView(_resources, monster));
		}
	}
}