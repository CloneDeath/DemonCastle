using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
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
		return variableDeclaration.Type switch {
			VariableType.Boolean => new BooleanVariableDeclarationDetails((BooleanVariableDeclarationInfo)variableDeclaration),
			VariableType.Integer => new IntegerVariableDeclarationDetails((IntegerVariableDeclarationInfo)variableDeclaration),
			VariableType.Float => new FloatVariableDeclarationDetails((FloatVariableDeclarationInfo)variableDeclaration),
			VariableType.String => new StringVariableDeclarationDetails((StringVariableDeclarationInfo)variableDeclaration),
			VariableType.Monster => new MonsterVariableDeclarationDetails(_project, (MonsterVariableDeclarationInfo)variableDeclaration),
			VariableType.Item => new ItemVariableDeclarationDetails(_project, (ItemVariableDeclarationInfo)variableDeclaration),
			VariableType.Vector2I => new Vector2IVariableDeclarationDetails((Vector2IVariableDeclarationInfo)variableDeclaration),
			_ => throw new InvalidEnumValueException<VariableType>(variableDeclaration.Type)
		};
	}
}