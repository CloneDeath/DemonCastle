using DemonCastle.ProjectFiles.Files.Conditions;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor.WhenClauses;

public class ThisMonsterWhenClause : IWhenClause {
	public string Clause => "this monster";
	public bool IsSelected(WhenInfo when) => when.Self != null;

	public void MakeSelected(WhenInfo when) {
		if (when.Self != null) return;
		when.Self = SelfEvent.Killed;
	}

	public void MakeUnselected(WhenInfo when) {
		when.Self = null;
	}

	public Control GetControl(WhenInfo when) {
		var control = new HBoxContainer();
		control.AddChild(new Label { Text = "is" });

		var options = new OptionButton();
		options.AddItem("killed");

		control.AddChild(options);
		return control;
	}
}