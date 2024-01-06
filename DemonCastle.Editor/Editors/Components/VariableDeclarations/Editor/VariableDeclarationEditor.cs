using System;
using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;

public partial class VariableDeclarationEditor : Control {
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

	private static VariableDeclarationDetails GetEditor(VariableDeclarationInfo variableDeclaration) {
		return variableDeclaration.DataType switch {
			VariableDataType.Boolean => new BooleanVariableDeclarationDetails(variableDeclaration),
			VariableDataType.Integer => new IntegerVariableDeclarationDetails(variableDeclaration),
			VariableDataType.Decimal => new DecimalVariableDeclarationDetails(variableDeclaration),
			VariableDataType.String => new StringVariableDeclarationDetails(variableDeclaration),
			//VariableDataType.Monster => expr,
			//VariableDataType.Item => expr,
			_ => throw new NotSupportedException()
		};
	}
}