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
				"this entity",
				when.Self != null,
				c => {
					when.Self ??= SelfEvent.Killed;
					c.AddChild(new ChoiceEnum<SelfEvent>(when.Self, e => when.Self = e));
				}
			},
			{
				"this entity's animation",
				when.Animation != null,
				c => {
					when.Animation ??= AnimationEvent.Complete;
					c.AddChild(new ChoiceEnum<AnimationEvent>(when.Animation, e => when.Animation = e));
				}
			},
			{
				"a random timer",
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
				"condition",
				when.Condition.IsSet,
				c => {
					when.Condition.IsSet = true;

					c.AddChild(new ChoiceTree {
						{
							"value",
							when.Condition.Value != null,
							i => {
								when.Condition.Value ??= true;
								var binding = new CallbackBinding<bool>(
									() => when.Condition.Value ?? false,
									(b) => when.Condition.Value = b);
								i.AddChild(new BooleanProperty(binding));
							}
						},
						{
							"variable",
							when.Condition.Variable != null,
							i => {
								when.Condition.Variable ??= Guid.Empty;

								var variables = Entity.Variables.Concat(_project.Variables);
								i.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
									variables.Where(v => v.Type == VariableType.Boolean),
									v => when.Condition.Variable == v.Id,
									v => when.Condition.Variable = v.Id));
							}
						}
					});
				}
			}
		});
	}
}