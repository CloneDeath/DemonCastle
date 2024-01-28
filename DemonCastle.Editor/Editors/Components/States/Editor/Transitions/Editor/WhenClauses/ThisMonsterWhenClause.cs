using DemonCastle.Files.Conditions.Events;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Components.States.Editor.Transitions.Editor.WhenClauses;

public class ThisMonsterWhenClause : IWhenClause {
	public string Clause => "this entity";
	public bool IsSelected(WhenInfo when) => when.Self != null;

	public void MakeSelected(WhenInfo when) {
		if (when.Self != null) return;
		when.Self = SelfEvent.Killed;
	}

	public void MakeUnselected(WhenInfo when) {
		when.Self = null;
	}

	public Control GetControl() {
		var control = new HBoxContainer();
		control.AddChild(new Label { Text = "is" });

		var options = new OptionButton();
		options.AddItem("killed");

		control.AddChild(options);
		return control;
	}
}