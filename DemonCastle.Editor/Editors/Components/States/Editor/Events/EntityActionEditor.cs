using System;
using System.Collections.Generic;
using System.Linq;
using DemonCastle.Editor.Editors.Components.Properties.Vector;
using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionEditor : ActionEditor<EntityActionInfo> {
	public EntityActionEditor(ProjectResources resources, IBaseEntityInfo entity, EntityActionInfo action, EntityActionInfoCollection actionSet) : base(actionSet, action) {
		Name = nameof(EntityActionEditor);

		AddChild(new ChoiceTree {
			{
				nameof(action.Face),
				action.Face != null,
				c => {
					action.Face ??= FaceAction.TowardsClosestPlayer;
					c.AddChild(new ChoiceEnum<FaceAction>(action.Face, v => action.Face = v));
				}
			},
			{
				nameof(action.Move),
				action.Move != null,
				c => {
					action.Move ??= MoveAction.Forward;
					c.AddChild(new ChoiceEnum<MoveAction>(action.Move, v => action.Move = v));
				}
			},
			{
				nameof(action.Self),
				action.Self != null,
				c => {
					action.Self ??= SelfAction.Despawn;
					c.AddChild(new ChoiceEnum<SelfAction>(action.Self, v => action.Self = v));
				}
			},
			{
				nameof(action.SpawnItem),
				action.SpawnItem.IsSet,
				c => {
					action.SpawnItem.IsSet = true;
                    SetUpSpawnControls(c, action.SpawnItem, resources.Items, entity, VariableType.Item);
				}
			},
			{
				nameof(action.SpawnMonster),
				action.SpawnMonster.IsSet,
				c => {
					action.SpawnMonster.IsSet = true;
                    SetUpSpawnControls(c, action.SpawnMonster, resources.Monsters, entity, VariableType.Monster);
				}
			}
		});
	}

	protected virtual void SetUpSpawnControls<T>(Control c, ActionSpawnInfo spawnInfo, IEnumerable<T> options, IBaseEntityInfo entity, VariableType type)
		where T : IBaseEntityInfo {
		/* Instance */
		c.AddChild(new ChoiceTree {
			{
				"Id",
				spawnInfo.Instance.Id != null,
				i => {
					spawnInfo.Instance.Id ??= Guid.Empty;
					i.AddChild(new ChoiceReferenceList<T>(
						options,
						v => v.Id == spawnInfo.Instance.Id,
						v => spawnInfo.Instance.Id = v.Id));
				}
			},
			{
				"Variable",
				spawnInfo.Instance.Variable != null,
				i => {
					spawnInfo.Instance.Variable ??= Guid.Empty;
					i.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
						entity.Variables.Where(v => v.Type == type),
						v => v.Id == spawnInfo.Instance.Variable,
						v => spawnInfo.Instance.Variable = v.Id));
				}
			}
		});

		/* Offset */
		c.AddChild(new Label { Text = "with offset"});
		c.AddChild(new ChoiceTree {
			{
				"Value",
				spawnInfo.Offset.Value != null,
				o => {
					spawnInfo.Offset.Value ??= Vector2I.Zero;
					var binding = new CallbackBinding<Vector2I>(
						() => spawnInfo.Offset.Value ?? Vector2I.Zero,
						v => spawnInfo.Offset.Value = v);
					o.AddChild(new Vector2IProperty(binding, new Vector2IPropertyOptions {
						AllowNegative = true
					}));
				}
			},
			{
				"Variable",
				spawnInfo.Offset.Variable != null,
				o => {
					spawnInfo.Offset.Variable ??= Guid.Empty;
					o.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
						entity.Variables.Where(v => v.Type == VariableType.Vector2I),
						v => v.Id == spawnInfo.Instance.Variable,
						v => spawnInfo.Instance.Variable = v.Id));
				}
			}
		});

		/* Relative To */
		c.AddChild(new Label { Text = "relative to" });
		c.AddChild(new ChoiceEnum<RelativeTo>(spawnInfo.RelativeTo, v => spawnInfo.RelativeTo = v));
	}
}