using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data.Levels;
using DemonCastle.ProjectFiles.Projects.Data.Levels.Monsters;
using Godot;

namespace DemonCastle.Editor.Editors.Level.Area.Tools.MonsterTools.List;

public partial class MonsterDataList : VBoxContainer {
	public event Action<MonsterDataInfo?>? MonsterSelected;

	private AreaInfo? _area;

	private Button AddMonster { get; }
	private ItemList Monsters { get; }
	private Button RemoveMonster { get; }

	public MonsterDataList() {
		AddChild(AddMonster = new Button { Text = "Add Monster"});
		AddMonster.Pressed += AddMonster_OnPressed;

		AddChild(Monsters = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Monsters.ItemSelected += Monsters_OnItemSelected;

		AddChild(RemoveMonster = new Button { Text = "Remove Monster"});
		RemoveMonster.Pressed += RemoveMonster_OnPressed;
	}

	private void Monsters_OnItemSelected(long index) {
		var monster = _area?.Monsters[(int)index];
		MonsterSelected?.Invoke(monster);
	}

	private void AddMonster_OnPressed() {
		_area?.Monsters.AppendNew();
	}

	private void RemoveMonster_OnPressed() {
		var selected = Monsters.GetSelectedItems();
		if (!selected.Any()) return;

		_area?.Monsters.RemoveAt(selected[0]);
	}

	public override void _ExitTree() {
		base._ExitTree();
		if (_area != null) {
			_area.Monsters.CollectionChanged -= Monsters_OnCollectionChanged;
		}
	}

	public void Load(AreaInfo? area) {
		if (_area != null) {
			_area.Monsters.CollectionChanged -= Monsters_OnCollectionChanged;
		}
		_area = area;
		if (_area != null) {
			_area.Monsters.CollectionChanged += Monsters_OnCollectionChanged;
		}
		Reload();
	}

	private void Monsters_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		Reload();
	}

	private void Reload() {
		Monsters.Clear();

		if (_area == null) return;
		foreach (var frame in _area.Monsters) {
			Monsters.AddItem(frame.MonsterId.ToString());
		}
	}
}