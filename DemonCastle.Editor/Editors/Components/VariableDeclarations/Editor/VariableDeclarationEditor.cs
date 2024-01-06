using System;
using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;

public partial class VariableDeclarationEditor : Control {
	private readonly ProjectInfo _project;

	public VariableDeclarationEditor(ProjectInfo project) {
		_project = project;
	}

	public void Clear() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
	}
	public void Load(VariableDeclarationInfo variableDeclaration) {
		Clear();

		var editor = GetEditor(variableDeclaration);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
	}

	private VariableDeclarationDetails GetEditor(VariableDeclarationInfo variableDeclaration) {
		return variableDeclaration.DataType switch {
			VariableDataType.Boolean => new BooleanVariableDeclarationDetails(variableDeclaration),
			VariableDataType.Integer => new IntegerVariableDeclarationDetails(variableDeclaration),
			VariableDataType.Decimal => new DecimalVariableDeclarationDetails(variableDeclaration),
			VariableDataType.String => new StringVariableDeclarationDetails(variableDeclaration),
			VariableDataType.Monster => new MonsterVariableDeclarationDetails(_project, variableDeclaration),
			VariableDataType.Item => new ItemVariableDeclarationDetails(_project, variableDeclaration),
			VariableDataType.Vector2I => new Vector2IVariableDeclarationDetails(_project, variableDeclaration),
			_ => throw new NotSupportedException()
		};
	}
}