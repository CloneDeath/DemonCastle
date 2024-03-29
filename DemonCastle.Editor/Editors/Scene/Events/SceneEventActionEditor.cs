using System.Linq;
using DemonCastle.Editor.Editors.Components.Actions;
using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.Properties.File;
using DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor;
using DemonCastle.Editor.Editors.Scene.Events.Conditions;
using DemonCastle.Editor.Properties;
using DemonCastle.Files.Actions;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events;

public partial class SceneEventActionEditor : ActionEditor<SceneEventActionInfo> {
	public SceneEventActionEditor(IFileInfo file, ProjectInfo project, SceneEventActionInfo action, IEnumerableInfo<SceneEventActionInfo> collection) : base(collection, action) {
		Name = nameof(SceneEventActionEditor);

		AddChild(new ChoiceTree {
			{
				"Game",
				action.Game.HasValue,
				gameControl => {
					action.Game ??= GameAction.Quit;
					var binding = new CallbackBinding<GameAction>(() => action.Game.Value, v => action.Game = v);
					gameControl.AddChild(new EnumProperty<GameAction>(binding) {
						SizeFlagsHorizontal = SizeFlags.ExpandFill
					});
				}
			},
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
			},
			{
				"Set Global Variable",
				action.SetGlobalVariable.IsSet,
				c => {
					action.SetGlobalVariable.IsSet = true;

					var variables = project.Variables;
					c.AddChild(new ChoiceReferenceList<VariableDeclarationInfo>(
						variables.Where(v => v.Type == VariableType.Boolean),
						v => action.SetGlobalVariable.VariableId == v.Id,
						v => action.SetGlobalVariable.VariableId = v.Id));

					c.AddChild(new ChoiceTree {
						{
							"Boolean",
							action.SetGlobalVariable.Type == VariableType.Boolean,
							i => {
								action.SetGlobalVariable.Type = VariableType.Boolean;
								i.AddChild(new BooleanConditionTree(action.SetGlobalVariable.BooleanValue, project.Variables));
							}
						}
					});
				}
			}
		});
	}
}