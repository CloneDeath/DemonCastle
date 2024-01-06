using DemonCastle.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor.WhenClauses;

public class ThisMonstersAnimationWhenClause : IWhenClause {
	public string Clause => "this monster's animation";
	public bool IsSelected(WhenInfo when) => when.Animation != null;

	public void MakeSelected(WhenInfo when) {
		if (when.Animation != null) return;
		when.Animation = AnimationEvent.Complete;
	}

	public void MakeUnselected(WhenInfo when) {
		when.Animation = null;
	}

	public Control GetControl(WhenInfo when) {
		var control = new HBoxContainer();
		control.AddChild(new Label { Text = "is" });

		var options = new OptionButton();
		options.AddItem("complete");

		control.AddChild(options);
		return control;
	}
}