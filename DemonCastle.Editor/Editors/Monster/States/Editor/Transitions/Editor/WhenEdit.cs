using DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor.WhenClauses;
using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor;

public partial class WhenEdit : HBoxContainer {
	private readonly TransitionInfoProxy _proxy = new();

	public TransitionInfo? Transition {
		get => _proxy.Proxy;
		set {
			_proxy.Proxy = value;
			Reload();
		}
	}

	protected OptionButton Clause { get; }
	protected HBoxContainer ClauseParams { get; }

	private static readonly IWhenClause[] Clauses = {
		new ThisMonsterWhenClause(),
		new ThisMonstersAnimationWhenClause(),
		new ARandomTimerWhenClause()
	};

	public WhenEdit() {
		Name = nameof(WhenEdit);

		AddChild(new Label { Text = "When"});
		AddChild(Clause = new OptionButton {
			Selected = -1
		});
		Clause.ItemSelected += Clause_OnItemSelected;
		foreach (var clause in Clauses) {
			Clause.AddItem(clause.Clause);
		}

		AddChild(ClauseParams = new HBoxContainer());
	}

	private void Clause_OnItemSelected(long index) {
		foreach (var child in ClauseParams.GetChildren()) {
			child.QueueFree();
		}

		var when = _proxy.When;
		if (when == null) {
			Clause.Selected = -1;
			return;
		}

		for (var i = 0; i < Clauses.Length; i++) {
			var clause = Clauses[i];
			if (i == index) {
				if (clause.IsSelected(when)) return;
				clause.MakeSelected(when);
				ClauseParams.AddChild(clause.GetControl(when));
			} else {
				if (!clause.IsSelected(when)) return;
				clause.MakeUnselected(when);
			}
		}
	}

	private void Reload() {
		foreach (var child in ClauseParams.GetChildren()) {
			child.QueueFree();
		}

		Clause.Selected = -1;
		var when = _proxy.When;
		if (when == null) {
			return;
		}

		for (var i = 0; i < Clauses.Length; i++) {
			var clause = Clauses[i];
			if (!clause.IsSelected(when)) continue;

			clause.MakeSelected(when);
			ClauseParams.AddChild(clause.GetControl(when));
			Clause.Selected = i;
			break;
		}
	}
}