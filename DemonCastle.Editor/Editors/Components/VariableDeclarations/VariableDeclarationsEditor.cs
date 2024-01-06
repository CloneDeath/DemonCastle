using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations;

public partial class VariableDeclarationsEditor : HSplitContainer {
	protected EnumerableInfoListByEnum<VariableDeclarationInfo, VariableDataType> VariableList { get; }
	protected VariableDeclarationEditor VariableEditor { get; }

	public VariableDeclarationsEditor(ProjectInfo project, IEnumerableInfoByEnum<VariableDeclarationInfo, VariableDataType> variables) {
		Name = nameof(VariableDeclarationsEditor);

		AddChild(VariableList = new EnumerableInfoListByEnum<VariableDeclarationInfo, VariableDataType>(variables){
			CustomMinimumSize = new Vector2(300, 300)
		});
		VariableList.ItemSelected += VariableList_OnItemSelected;

		AddChild(VariableEditor = new VariableDeclarationEditor(project) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	protected void VariableList_OnItemSelected(VariableDeclarationInfo? variableDeclaration) {
		if (variableDeclaration != null) VariableEditor.Load(variableDeclaration);
		else VariableEditor.Clear();
	}
}