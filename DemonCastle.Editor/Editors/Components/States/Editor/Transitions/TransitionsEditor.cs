using DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions;

public partial class TransitionsEditor : HSplitContainer {
	private readonly StateInfoProxy _state = new();
	private readonly EnumerableInfoProxy<EntityStateInfo> _entityStates = new();

	public EntityStateInfo? State {
		get => _state.Proxy;
		set => _state.Proxy = value;
	}

	public IEnumerableInfo<EntityStateInfo>? EntityStates {
		get => _entityStates.Proxy;
		set => _entityStates.Proxy = value;
	}

	private InfoCollectionEditor<EntityStateTransitionInfo> Transitions { get; }
	private TransitionEdit TransitionEdit { get; }

	public TransitionsEditor() {
		Name = nameof(TransitionsEditor);

		AddChild(Transitions = new InfoCollectionEditor<EntityStateTransitionInfo>(_state.Transitions) {
			CustomMinimumSize = new Vector2(300, 0)
		});
		Transitions.ItemSelected += Transitions_OnTransitionSelected;

		AddChild(TransitionEdit = new TransitionEdit(_entityStates) {
			SizeFlagsVertical = SizeFlags.ExpandFill
		});
	}

	private void Transitions_OnTransitionSelected(EntityStateTransitionInfo? obj) {
		TransitionEdit.Transition = obj;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		Transitions.Enabled = _state.Proxy != null;
	}
}