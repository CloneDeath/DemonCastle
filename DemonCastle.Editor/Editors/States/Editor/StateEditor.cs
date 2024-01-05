using System.Collections.Generic;
using DemonCastle.Editor.Editors.States.Editor.Events;
using DemonCastle.Editor.Editors.States.Editor.Transitions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.Animations;
using DemonCastle.ProjectFiles.Projects.Data.States;
using Godot;

namespace DemonCastle.Editor.Editors.States.Editor;

public partial class StateEditor : VSplitContainer {
	private StateDetails Details { get; }
	private TabContainer TabContainer { get; }
	private EventsEditor EventsEditor { get; }
	private TransitionsEditor TransitionsEditor { get; }

	public StateEditor(IEnumerableInfo<IAnimationInfo> animations, IEnumerable<StateInfo> states) {
		Name = nameof(StateEditor);

		AddChild(Details = new StateDetails(animations));

		AddChild(TabContainer = new TabContainer());
		TabContainer.AddChild(EventsEditor = new EventsEditor());
		TabContainer.SetTabTitle(0, "Events");
		TabContainer.AddChild(TransitionsEditor = new TransitionsEditor(states));
		TabContainer.SetTabTitle(1, "Transitions");
	}

	public void LoadState(StateInfo state) {
		Details.State = state;
		EventsEditor.State = state;
		TransitionsEditor.State = state;
	}
}