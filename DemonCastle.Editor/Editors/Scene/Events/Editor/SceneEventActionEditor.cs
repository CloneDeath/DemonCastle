using System;
using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;
using FileProperty = DemonCastle.Editor.Editors.Components.Properties.File.FileProperty;
using IntegerProperty = DemonCastle.Editor.Editors.Components.Properties.IntegerProperty;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventActionEditor : HFlowContainer {
	public event Action? Deleted;

	private readonly Button DeleteButton;

	public SceneEventActionEditor(IFileInfo file, SceneEventActionInfo action) {
		Name = nameof(SceneEventActionEditor);
		AddChild(DeleteButton = new Button { Text = "X" });
		DeleteButton.Pressed += () => Deleted?.Invoke();

		AddChild(new ChoiceTree {
			{
				"Scene",
				action.Scene.IsSet,
				sceneControl => {
					action.Scene.IsSet = true;
					sceneControl.AddChild(new Label { Text = "should" });
					sceneControl.AddChild(new ChoiceTree {
						{
							"Set",
							action.Scene.Set != null,
							setControl => {
								action.Scene.Set ??= string.Empty;
								setControl.AddChild(new Label { Text = "to" });
								var binding = new CallbackBinding<string>(() => action.Scene.Set, v => action.Scene.Set = v);
								setControl.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Scene }){
									SizeFlagsHorizontal = SizeFlags.ExpandFill
								});
							}
						},
						{
							"Push",
							action.Scene.Push != null,
							pushControl => {
								action.Scene.Push ??= string.Empty;
								var binding = new CallbackBinding<string>(() => action.Scene.Push, v => action.Scene.Push = v);
								pushControl.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Scene }) {
									SizeFlagsHorizontal = SizeFlags.ExpandFill
								});
							}
						},
						{
							"Pop",
							action.Scene.Pop != null,
							popControl => {
								action.Scene.Pop ??= 1;
								var binding = new CallbackBinding<int>(() => action.Scene.Pop ?? 1, v => action.Scene.Pop = v);
								popControl.AddChild(new IntegerProperty(binding));
								popControl.AddChild(new Label { Text = "scenes" });
							}
						}
					});
				}
			},
			{
				"Set Character",
				action.SetCharacter != null,
				c => {
					action.SetCharacter ??= string.Empty;
					var binding = new CallbackBinding<string>(() => action.SetCharacter, v => action.SetCharacter = v);
					c.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Character }) {
						SizeFlagsHorizontal = SizeFlags.ExpandFill
					});
				}
			},
			{
				"Set Level",
				action.SetLevel != null,
				c => {
					action.SetLevel ??= string.Empty;
					var binding = new CallbackBinding<string>(() => action.SetLevel, v => action.SetLevel = v);
					c.AddChild(new FileProperty(binding, file.Directory, new[] { FileType.Level }) {
						SizeFlagsHorizontal = SizeFlags.ExpandFill
					});
				}
			}
		});
	}
}