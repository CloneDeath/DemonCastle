using DemonCastle.Editor.Editors.Components.Animations;
using DemonCastle.Editor.Editors.Components.BaseEntity.Events;
using DemonCastle.Editor.Editors.Components.States;
using DemonCastle.Editor.Editors.Components.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public partial class BaseEntityTabContainer : TabContainer {
	private readonly VariableCollectionEditor _variables;
	private readonly AnimationsEditor _animations;
	private readonly StatesEditor _states;
	private readonly EventsEditor _events;

	public BaseEntityTabContainer(ProjectResources resources, ProjectInfo project, IFileInfo file) {
		Name = nameof(BaseEntityTabContainer);

		AddTab("Variables", _variables = new VariableCollectionEditor(resources));
		AddTab("Animations", _animations = new AnimationsEditor(file));
		AddTab("States", _states = new StatesEditor(resources, project));
		AddTab("Events", _events = new EventsEditor(resources));
		CurrentTab = _animations.GetIndex();
	}

	public void AddTab(string title, Control control) {
		AddChild(control);
		SetTabTitle(control.GetIndex(), title);
	}

	public void Load(IBaseEntityInfo? entity) {
		_variables.Load(entity?.Variables);
		_animations.Load(entity?.Animations);
		_states.Load(entity);
		_events.Load(entity);
	}
}