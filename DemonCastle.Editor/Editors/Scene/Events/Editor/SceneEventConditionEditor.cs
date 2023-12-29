using System;
using DemonCastle.Editor.Editors.Scene.Events.Editor.Conditions;
using DemonCastle.ProjectFiles.Files.SceneEvents;
using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventConditionEditor : HFlowContainer {
	public SceneEventConditionEditor(SceneEventConditionInfo when) {
		Name = nameof(SceneEventConditionEditor);
		AddChild(new Label { Text = "When" });

		AddChild(new ChoiceTree {
			{ "All of",
				when.And != null,
				c => {
					when.And = Array.Empty<SceneEventConditionData>();
					c.AddChild(new Label { Text = "All Of" });
				}
			},
			{ "Any of",
				when.Or != null,
				c => {
					when.Or = Array.Empty<SceneEventConditionData>();
					c.AddChild(new Label { Text = "Any Of" });
				}
			},
			{ "the Action",
				when.Input != null,
				c => {
					when.Input = new InputConditionData();
					c.AddChild(new Label { Text = "The Action" });
				}
			},
			{ "any Action",
				when.AnyInput != null,
				c => {
					c.AddChild(new Label { Text = "is" });
					c.AddChild(new ChoiceTree {
						{ "Pressed",
							when.AnyInput == KeyState.Pressed,
							_ => {
								when.AnyInput = KeyState.Pressed;
							}
						},
						{ "Released",
							when.AnyInput == KeyState.Released,
							_ => {
								when.AnyInput = KeyState.Released;
							}
						},
						{ "Down",
							when.AnyInput == KeyState.Down,
							_ => {
								when.AnyInput = KeyState.Down;
							}
						},
						{ "Up",
							when.AnyInput == KeyState.Up,
							_ => {
								when.AnyInput = KeyState.Up;
							}
						}
					});
				}
			}
		});

		/*
		OptionButton optionButton;
		AddChild(optionButton = new OptionButton());
		optionButton.AddItem("All of");
		optionButton.AddItem("Any of");
		optionButton.AddItem("the Action");
		optionButton.AddItem("any Action");
		optionButton.Selected = -1;

		AddChild(new Label{Text = "is"});
		OptionButton optionButton2;
		AddChild(optionButton2 = new OptionButton());
		optionButton2.AddItem("Pressed");
		optionButton2.AddItem("Released");
		optionButton2.AddItem("Down");
		optionButton2.AddItem("Up");

		AddChild(new Label{Text = "("});
		OptionButton optionButton3;
		AddChild(optionButton3 = new OptionButton());
		optionButton3.AddItem("Up");
		optionButton3.AddItem("Down");
		optionButton3.AddItem("Left");
		optionButton3.AddItem("Right");
		optionButton3.AddItem("A");
		optionButton3.AddItem("B");
		optionButton3.AddItem("X");
		optionButton3.AddItem("Y");
		optionButton3.AddItem("Start");
		optionButton3.AddItem("Select");
		AddChild(new Label{Text = ")"});
		*/
	}
}