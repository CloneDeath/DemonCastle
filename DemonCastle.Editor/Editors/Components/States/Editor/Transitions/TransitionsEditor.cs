using System.Collections.Generic;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions.List;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions;

public partial class TransitionsEditor : HSplitContainer {
	private readonly StateInfoProxy _proxy = new();

	public EntityStateInfo? State {
		get => _proxy.Proxy;
		set => _proxy.Proxy = value;
	}

	private TransitionList Transitions { get; }
	private TransitionEdit TransitionEdit { get; }

	public TransitionsEditor(IEnumerable<EntityStateInfo> options) {
		Name = nameof(TransitionsEditor);

		AddChild(Transitions = new TransitionList(_proxy.Transitions) {
			CustomMinimumSize = new Vector2(300, 0)
		});
		Transitions.ItemSelected += Transitions_OnTransitionSelected;

		AddChild(TransitionEdit = new TransitionEdit(options) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}

	private void Transitions_OnTransitionSelected(EntityStateTransitionInfo obj) {
		TransitionEdit.Transition = obj;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Transitions.Enabled = _proxy.Proxy != null;
	}
}