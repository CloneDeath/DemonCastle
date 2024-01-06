using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
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
					c.AddChild(new ChoiceEnum<FaceAction>(entityAction.Face, v => entityAction.Face = v));
				}
			},
			{
				nameof(entityAction.Move),
				entityAction.Move != null,
				c => {
					c.AddChild(new ChoiceEnum<MoveAction>(entityAction.Move, v => entityAction.Move = v));
				}
			},
			{
				nameof(entityAction.Self),
				entityAction.Self != null,
				c => {
					c.AddChild(new ChoiceEnum<SelfAction>(entityAction.Self, v => entityAction.Self = v));
				}
			}
		});
	}
}