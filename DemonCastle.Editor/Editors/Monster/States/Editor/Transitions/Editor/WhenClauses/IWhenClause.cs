using DemonCastle.ProjectFiles.Projects.Data.States.Transitions;
using Godot;

namespace DemonCastle.Editor.Editors.Monster.States.Editor.Transitions.Editor.WhenClauses;

public interface IWhenClause {
	public string Clause { get; }
	public bool IsSelected(WhenInfo when);
	public void MakeSelected(WhenInfo when);
	public void MakeUnselected(WhenInfo when);
	public Control GetControl(WhenInfo when);
}