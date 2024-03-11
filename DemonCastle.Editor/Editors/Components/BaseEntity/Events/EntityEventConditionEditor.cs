using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Data.Events;
using Godot;

namespace DemonCastle.Editor.Editors.Components.BaseEntity.Events;

public partial class EntityEventConditionEditor : HFlowContainer {
	public EntityEventConditionEditor(EntityEventConditionInfo when) {
		Name = nameof(EntityEventConditionEditor);
		AddChild(new Label { Text = "When" });

		AddChild(new ChoiceTree {
			{ "the Player",
				when.OnPlayer != null,
				c => {
					when.OnPlayer ??= PlayerPositionTransition.Enter;
					c.AddChild(new ChoiceEnum<PlayerPositionTransition>(when.OnPlayer, v => when.OnPlayer = v));
				}
			}
		});
	}
}