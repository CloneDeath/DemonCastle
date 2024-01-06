using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;

public partial class VariableDeclarationEditor : Control {
	public void LoadState(VariableDeclarationInfo variableDeclaration) {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}
}