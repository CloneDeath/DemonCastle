using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations;

public partial class VariableDeclarationsEditor : HSplitContainer {
	protected EnumerableInfoList<VariableDeclarationInfo> VariableList { get; }
	protected VariableDeclarationEditor VariableEditor { get; }

	public VariableDeclarationsEditor(IEnumerableInfo<VariableDeclarationInfo> variables) {
		Name = nameof(VariableDeclarationsEditor);

		AddChild(VariableList = new EnumerableInfoList<VariableDeclarationInfo>(variables){
			CustomMinimumSize = new Vector2(300, 300)
		});
		VariableList.ItemSelected += VariableList_OnItemSelected;

		AddChild(VariableEditor = new VariableDeclarationEditor {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void VariableList_OnItemSelected(VariableDeclarationInfo variableDeclaration) {
		VariableEditor.Load(variableDeclaration);
	}
}