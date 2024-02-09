using System.Linq;
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
		var selected = VariableList.SelectedItem;
		VariableList.Load(variables);
		var newSelection = variables?.FirstOrDefault(v => v.Name == selected?.Name)
						   ?? variables?.FirstOrDefault(a => a.Name.ToLower() == "default")
						   ?? variables?.FirstOrDefault();
		VariableList.SelectedItem = newSelection;
		VariableEditor.VariableDeclaration = newSelection;
	}

	protected void VariableList_OnItemSelected(VariableDeclarationInfo? variableDeclaration) {
		VariableEditor.VariableDeclaration = variableDeclaration;
	}
}