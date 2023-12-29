using DemonCastle.Editor.Editors.Properties.File;
using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;
using IntegerProperty = DemonCastle.Editor.Editors.Properties.IntegerProperty;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventActionEditor : HFlowContainer {
	public SceneEventActionEditor(IFileInfo file, SceneEventActionInfo then) {
		Name = nameof(SceneEventActionEditor);
		AddChild(new Label{Text = "Then"});

		AddChild(new ChoiceTree {
			{
				"Scene",
				then.Scene.IsSet,
				sceneControl => {
					then.Scene.IsSet = true;
					sceneControl.AddChild(new Label { Text = "should" });
					sceneControl.AddChild(new ChoiceTree {
						{
							"Set",
							then.Scene.Set != null,
							setControl => {
								then.Scene.Set ??= string.Empty;
								setControl.AddChild(new Label { Text = "to" });
								var binding = new CallbackBinding<string>(() => then.Scene.Set, v => then.Scene.Set = v);
								setControl.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Scene }){
									SizeFlagsHorizontal = SizeFlags.ExpandFill
								});
							}
						},
						{
							"Push",
							then.Scene.Push != null,
							pushControl => {
								then.Scene.Push ??= string.Empty;
								var binding = new CallbackBinding<string>(() => then.Scene.Push, v => then.Scene.Push = v);
								pushControl.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Scene }) {
									SizeFlagsHorizontal = SizeFlags.ExpandFill
								});
							}
						},
						{
							"Pop",
							then.Scene.Pop != null,
							popControl => {
								then.Scene.Pop ??= 1;
								var binding = new CallbackBinding<int>(() => then.Scene.Pop ?? 1, v => then.Scene.Pop = v);
								popControl.AddChild(new IntegerProperty(binding));
								popControl.AddChild(new Label { Text = "scenes" });
							}
						}
					});
				}
			}
		});
	}
}