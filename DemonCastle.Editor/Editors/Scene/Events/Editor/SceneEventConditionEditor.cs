using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
using DemonCastle.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventConditionEditor : HFlowContainer {
	public SceneEventConditionEditor(SceneEventConditionInfo when) {
		Name = nameof(SceneEventConditionEditor);
		AddChild(new Label { Text = "When" });

		AddChild(new ChoiceTree {
			/*{ "All Of",
				when.And != null,
				c => {
					when.And ??= Array.Empty<SceneEventConditionData>();
					c.AddChild(new Label { Text = "All Of (NOT IMPLEMENTED)" });
				}
			},
			{ "Any Of",
				when.Or != null,
				c => {
					when.Or ??= Array.Empty<SceneEventConditionData>();
					c.AddChild(new Label { Text = "Any Of (NOT IMPLEMENTED)" });
				}
			},*/
			{ "The Action",
				when.Input != null,
				c => {
					when.Input ??= new InputConditionData();
					c.AddChild(new ChoiceEnum<PlayerAction>(when.Input.Action, v => when.Input.Action = v));
					c.AddChild(new Label { Text = "is" });
					c.AddChild(new ChoiceEnum<KeyState>(when.Input.State, v => when.Input.State = v));
				}
			},
			{ "Any Action",
				when.AnyInput != null,
				c => {
					c.AddChild(new Label { Text = "is" });
					c.AddChild(new ChoiceEnum<KeyState>(when.AnyInput, v => when.AnyInput = v));
				}
			},
			{ "This Scene",
				when.ThisScene != null,
				c => {
					c.AddChild(new Label { Text = "on" });
					c.AddChild(new ChoiceEnum<SceneChangeEvent>(when.ThisScene, v => when.ThisScene = v));
				}
			}
		});
	}
}