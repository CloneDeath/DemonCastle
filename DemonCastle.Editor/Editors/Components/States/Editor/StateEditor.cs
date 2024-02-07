using DemonCastle.Editor.Editors.Components.States.Editor.Events;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor;

public partial class StateEditor : VSplitContainer {
	private StateDetails Details { get; }
	private TabContainer TabContainer { get; }
	private EventsEditor EventsEditor { get; }
	private TransitionsEditor TransitionsEditor { get; }

	public StateEditor(ProjectResources resources, ProjectInfo project) {
		Name = nameof(StateEditor);

		AddChild(Details = new StateDetails());

		AddChild(TabContainer = new TabContainer());
		TabContainer.AddChild(EventsEditor = new EventsEditor(resources));
		TabContainer.SetTabTitle(0, "Events");
		TabContainer.AddChild(TransitionsEditor = new TransitionsEditor(project));
		TabContainer.SetTabTitle(1, "Transitions");
	}

	public void LoadEntity(IBaseEntityInfo? entity) {
		Details.Animations = entity?.Animations;
		EventsEditor.Entity = entity;
		TransitionsEditor.Entity = entity;
	}

	public void LoadState(EntityStateInfo? state) {
		Details.State = state;
		EventsEditor.State = state;
		TransitionsEditor.State = state;
	}
}