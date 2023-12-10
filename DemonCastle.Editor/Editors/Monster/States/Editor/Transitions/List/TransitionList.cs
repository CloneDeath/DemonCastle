using System;
using System.Collections.Specialized;
using System.Linq;
using DemonCastle.Editor.Editors.Monster.States.List;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.List;

public partial class TransitionList : VBoxContainer {
	public event Action<TransitionInfo>? TransitionSelected;

	private readonly IEnumerableInfo<TransitionInfo> _transitions;
	private ItemList Transitions { get; }
	private Button AddButton { get; }
	private Button RemoveButton { get; }

	private bool _enabled;
	public bool Enabled {
		get => _enabled;
		set {
			_enabled = value;
			AddButton.Disabled = !value;
			RemoveButton.Disabled = !value;
		}
	}

	public TransitionList(IEnumerableInfo<TransitionInfo> transitions) {
		_transitions = transitions;

		Name = nameof(StateList);

		AddChild(AddButton = new Button { Text = "Add" });
		AddButton.Pressed += AddButton_OnPressed;
		AddChild(Transitions = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		Transitions.ItemSelected += Transitions_OnItemSelected;
		AddChild(RemoveButton = new Button { Text = "Remove" });
		RemoveButton.Pressed += RemoveButton_OnPressed;

		ReloadTransitions();
	}

	private void Transitions_OnItemSelected(long index) {
		var transition = _transitions[(int)index];
		TransitionSelected?.Invoke(transition);
	}

	public override void _EnterTree() {
		base._EnterTree();
		_transitions.CollectionChanged += Transitions_OnCollectionChanged;
	}

	public override void _ExitTree() {
		base._ExitTree();
		_transitions.CollectionChanged -= Transitions_OnCollectionChanged;
	}

	private void AddButton_OnPressed() {
		_transitions.AppendNew();
	}

	private void RemoveButton_OnPressed() {
		var selected = Transitions.GetSelectedItems();
		if (!selected.Any()) return;

		_transitions.RemoveAt(selected[0]);
	}

	private void Transitions_OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
		ReloadTransitions();
	}

	private void ReloadTransitions() {
		Transitions.Clear();

		foreach (var transition in _transitions) {
			Transitions.AddItem($"[{transition.Name}]: -> {transition.TargetState}");
		}
	}
}