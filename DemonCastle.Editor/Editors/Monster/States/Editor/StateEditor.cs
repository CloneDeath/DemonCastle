using DemonCastle.Editor.Editors.Monster.States.Editor.Events;
using DemonCastle.Editor.Editors.Monster.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor;

public partial class StateEditor : VSplitContainer {
	private StateDetails Details { get; }
	private TabContainer TabContainer { get; }
	private EventsEditor EventsEditor { get; }
	private TransitionsEditor TransitionsEditor { get; }

	public StateEditor(IFileInfo file, IEnumerableInfo<IAnimationInfo> animations) {
		Name = nameof(StateEditor);

		AddChild(Details = new StateDetails(animations));

		AddChild(TabContainer = new TabContainer());
		TabContainer.AddChild(EventsEditor = new EventsEditor());
		TabContainer.SetTabTitle(0, "Events");
		TabContainer.AddChild(TransitionsEditor = new TransitionsEditor());
		TabContainer.SetTabTitle(1, "Transitions");
	}

	public void LoadState(StateInfo state) {
		Details.State = state;
		EventsEditor.State = state;
		TransitionsEditor.State = state;
	}
}