using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations;

public partial class VariableCollectionEditor : HSplitContainer {
	protected InfoCollectionEditorByEnum<VariableDeclarationInfo, VariableType> VariableList { get; }
	protected VariableDeclarationEditor VariableEditor { get; }

	public VariableCollectionEditor(ProjectResources resources) {
		Name = nameof(VariableCollectionEditor);

		AddChild(VariableList = new InfoCollectionEditorByEnum<VariableDeclarationInfo, VariableType> {
			CustomMinimumSize = new Vector2(300, 300)
		});
		VariableList.ItemSelected += VariableList_OnItemSelected;

		AddChild(VariableEditor = new VariableDeclarationEditor(resources) {
			CustomMinimumSize = new Vector2(300, 300)
		});
	}

	public void Load(IEnumerableInfoByEnum<VariableDeclarationInfo, VariableType>? variables) {
		VariableList.Load(variables);
	}

	protected void VariableList_OnItemSelected(VariableDeclarationInfo? variableDeclaration) {
		if (variableDeclaration != null) VariableEditor.Load(variableDeclaration);
		else VariableEditor.Clear();
	}
}