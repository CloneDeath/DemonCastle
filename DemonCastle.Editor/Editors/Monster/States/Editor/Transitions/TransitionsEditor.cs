using DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.List;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions;

public partial class TransitionsEditor : HSplitContainer {
	private readonly StateInfoProxy _proxy = new();

	public StateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	private TransitionList Transitions { get; }
	private ItemList When { get; }

	public TransitionsEditor() {
		Name = nameof(TransitionsEditor);

		AddChild(Transitions = new TransitionList(_proxy.Transitions) {
			CustomMinimumSize = new Vector2(300, 0)
		});
		Transitions.TransitionSelected += Transitions_OnTransitionSelected;

		AddChild(When = new ItemList {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
		When.AddItem("animation.isComplete");
	}

	private void Transitions_OnTransitionSelected(TransitionInfo obj) {

	}

	public override void _Process(double delta) {
		base._Process(delta);
		Transitions.Enabled = _proxy.Proxy != null;
	}
}