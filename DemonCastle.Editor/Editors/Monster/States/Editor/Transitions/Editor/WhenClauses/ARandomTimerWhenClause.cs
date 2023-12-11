using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor.WhenClauses;

public class ARandomTimerWhenClause : IWhenClause {
	public string Clause => "a random timer";
	public bool IsSelected(WhenInfo when) => when.RandomTimerExpires.IsSet;

	public void MakeSelected(WhenInfo when) {
		if (when.RandomTimerExpires.IsSet) return;
		when.RandomTimerExpires.IsSet = true;
	}

	public void MakeUnselected(WhenInfo when) {
		when.RandomTimerExpires.IsSet = false;
	}

	public Control GetControl(WhenInfo when) {
		var control = new HBoxContainer();
		control.AddChild(new Label { Text = "between" });
		control.AddChild(new SpinBox { Value = 1 });
		control.AddChild(new Label { Text = "and" });
		control.AddChild(new SpinBox { Value = 2 });
		control.AddChild(new Label { Text = "seconds" });
		return control;
	}
}