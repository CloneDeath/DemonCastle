using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.States.List;

public partial class StateList : VBoxContainer {
	public event Action<StateInfo>? StateSelected;

	private readonly IEnumerableInfo<StateInfo> _states;
	private ItemList Animations { get; }
	private Button AddButton { get; }
	private Button RemoveButton { get; }

	public StateList(IEnumerableInfo<StateInfo> states) {
		_states = states;

		Name = nameof(StateList);

		AddChild(AddButton = new Button { Text = "Add" });
		AddButton.Pressed += AddButton_OnPressed;
		AddChild(Animations = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Animations.ItemSelected += Animations_OnItemSelected;
		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadAnimations();
	}

	private void Animations_OnItemSelected(long index) {
		var animation = _states[(int)index];
		StateSelected?.Invoke(animation);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_states.CollectionChanged += States_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_states.CollectionChanged -= States_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		_states.AppendNew();
	}

	private void RemoveButton_OnPressed() {
		var selected = Animations.GetSelectedItems();
		if (!selected.Any()) return;

		_states.RemoveAt(selected[0]);
	}

	private void States_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadAnimations();
	}

	private void ReloadAnimations() {
		Animations.Clear();

		foreach (var animation in _states) {
			Animations.AddItem(animation.Name);
		}
	}
}