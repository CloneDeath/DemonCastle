using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventConditionEditor : HFlowContainer {
	public SceneEventConditionEditor(SceneEventConditionInfo when) {
		Name = nameof(SceneEventConditionEditor);
		AddChild(new Label { Text = "When" });

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
	}
}