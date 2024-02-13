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
		set {
			_state.Proxy = value;
			TransitionEdit.Transition = null;
		}
	}

	public IBaseEntityInfo? Entity {
		get => TransitionEdit.Entity;
		set {
			TransitionEdit.Entity = value;
			_entityStates.Proxy = value?.States;
		}
	}

	private InfoCollectionEditor<EntityStateTransitionInfo> Transitions { get; }
	private TransitionEdit TransitionEdit { get; }

	public TransitionsEditor(ProjectInfo project) {
		Name = nameof(TransitionsEditor);

		AddChild(Transitions = new InfoCollectionEditor<EntityStateTransitionInfo>(_state.Transitions) {
			CustomMinimumSize = new Vector2(300, 0)
		});
		Transitions.ItemSelected += Transitions_OnTransitionSelected;

		AddChild(TransitionEdit = new TransitionEdit(project, _entityStates) {
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