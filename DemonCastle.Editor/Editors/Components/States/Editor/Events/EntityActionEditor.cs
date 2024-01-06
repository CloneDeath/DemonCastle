using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Actions.ActionEnums;
using DemonCastle.ProjectFiles.Projects.Data.States.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Events;

public partial class EntityActionEditor : MarginContainer {
	public EntityActionEditor(EntityActionInfo entityAction) {
		Name = nameof(EntityActionEditor);

		AddChild(new ChoiceTree {
			{
				nameof(entityAction.Face),
				entityAction.Face != null,
				c => {
					entityAction.Face ??= FaceAction.TowardsClosestPlayer;
					c.AddChild(new ChoiceEnum<FaceAction>(entityAction.Face, v => entityAction.Face = v));
				}
			},
			{
				nameof(entityAction.Move),
				entityAction.Move != null,
				c => {
					entityAction.Move ??= MoveAction.Forward;
					c.AddChild(new ChoiceEnum<MoveAction>(entityAction.Move, v => entityAction.Move = v));
				}
			},
			{
				nameof(entityAction.Self),
				entityAction.Self != null,
				c => {
					entityAction.Self ??= SelfAction.Despawn;
					c.AddChild(new ChoiceEnum<SelfAction>(entityAction.Self, v => entityAction.Self = v));
				}
			},
			{
				nameof(entityAction.SpawnItem),
				entityAction.SpawnItem.IsSet,
				c => {
					entityAction.SpawnItem.IsSet = true;
					c.AddChild(new Label { Text = "relative to" });
					c.AddChild(new ChoiceEnum<RelativeTo>(entityAction.SpawnItem.RelativeTo, v => entityAction.SpawnItem.RelativeTo = v));
				}
			},
			{
				nameof(entityAction.SpawnMonster),
				entityAction.SpawnMonster.IsSet,
				c => {
					entityAction.SpawnMonster.IsSet = true;
					c.AddChild(new Label { Text = "relative to" });
					c.AddChild(new ChoiceEnum<RelativeTo>(entityAction.SpawnMonster.RelativeTo, v => entityAction.SpawnMonster.RelativeTo = v));
				}
			}
		});
	}
}