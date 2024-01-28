using System;
using System.Linq;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.Files.Conditions.Events;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;

public partial class WhenEdit : HFlowContainer {
	private readonly ProjectInfo _project;
	private readonly TransitionInfoProxy _proxy = new();
	private IBaseEntityInfo? _entity;

	public EntityStateTransitionInfo? Transition {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			Reload();
		}
	}

	public IBaseEntityInfo? Entity {
		get => _entity;
		set {
			_entity = value;
			Reload();
		}
	}

	public WhenEdit(ProjectInfo project) {
		_project = project;
		Name = nameof(WhenEdit);
	}

	private void Reload() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}

		if (Entity == null) return;

		var when = Transition?.When;
		if (when == null) return;

		AddChild(new Label { Text = "When"});
		AddChild(new ChoiceTree {
			{
				"this Entity",
				when.Self != null,
				c => {
					when.Self ??= SelfEvent.Killed;
					c.AddChild(new ChoiceEnum<SelfEvent>(when.Self, e => when.Self = e));
				}
			},
			{
				"this Entity's Animation",
				when.Animation != null,
				c => {
					when.Animation ??= AnimationEvent.Complete;
					c.AddChild(new ChoiceEnum<AnimationEvent>(when.Animation, e => when.Animation = e));
				}
			},
			{
				"a Random Timer",
				when.RandomTimerExpires.IsSet,
				c => {
					when.RandomTimerExpires.IsSet = true;

					c.AddChild(new Label { Text = "between" });
					c.AddChild(new SpinBox { Value = 1 });
					c.AddChild(new Label { Text = "and" });
					c.AddChild(new SpinBox { Value = 2 });
					c.AddChild(new Label { Text = "seconds expires" });
				}
			},
			{
				"Condition",
				when.Condition.IsSet,
				c => {
					when.Condition.IsSet = true;
					SetupCondition(c, when.Condition);
				}
			}
		});
	}

	private void SetupCondition(Node c, BooleanConditionInfo condition) {
		if (Entity == null) return;

		c.AddChild(new ChoiceTree {
			{
				"Value",
				condition.Value != null,
				i => {
					condition.Value ??= true;
					var binding = new CallbackBinding<bool>(
						() => condition.Value ?? false,
						(b) => condition.Value = b);
					i.AddChild(new BooleanProperty(binding));
				}
			},
			{
				"Variable",
				condition.Variable != null,
				i => {
					condition.Variable ??= Guid.Empty;

					var variables = Entity.Variables.Concat(_project.Variables);
					i.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
						variables.Where(v => v.Type == VariableType.Boolean),
						v => condition.Variable == v.Id,
						v => condition.Variable = v.Id));
				}
			},
			{
				"Not",
				condition.Not.IsSet,
				i => {
					condition.Not.IsSet = true;
					SetupCondition(i, condition.Not);
				}
			}
		});
	}
}