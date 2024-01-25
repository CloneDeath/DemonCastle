using DemonCastle.Editor.Editors.Components.Animations;
using DemonCastle.Editor.Editors.Components.States;
using DemonCastle.Editor.Editors.Components.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity;

public partial class BaseEntityTabContainer : TabContainer {
	private readonly VariableDeclarationsEditor _variables;
	private readonly AnimationsEditor _animations;
	private readonly StatesEditor _states;

	public BaseEntityTabContainer(ProjectInfo project, IFileInfo file) {
		Name = nameof(BaseEntityTabContainer);

		AddChild(_variables = new VariableDeclarationsEditor(project));
		SetTabTitle(0, "Variables");
		AddChild(_animations = new AnimationsEditor(file));
		SetTabTitle(1, "Animations");
		AddChild(_states = new StatesEditor(project));
		SetTabTitle(2, "States");
	}

	public void Load(IBaseEntityInfo? entity) {
		_variables.Load(entity?.Variables);
		_animations.Load(entity?.Animations);
		_states.Load(entity);
	}
}