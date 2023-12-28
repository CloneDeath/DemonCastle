using DemonCastle.ProjectFiles.Projects.Data.SceneEvents;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Events.Editor;

public partial class SceneEventActionEditor : HFlowContainer {
	public SceneEventActionEditor(SceneEventActionInfo then) {
		Name = nameof(SceneEventActionEditor);
		AddChild(new Label{Text = "Then"});


		OptionButton optionButton;
		AddChild(optionButton = new OptionButton());
		optionButton.AddItem("Scene");

		OptionButton optionButton2;
		AddChild(optionButton2 = new OptionButton());
		optionButton2.AddItem("Set");
		optionButton2.AddItem("Push");
		optionButton2.AddItem("Pop");

		AddChild(new LineEdit {Text = "other_scene.dcs"});
	}
}