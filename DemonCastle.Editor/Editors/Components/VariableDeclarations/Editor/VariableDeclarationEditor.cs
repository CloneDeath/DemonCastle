using DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;
using DemonCastle.Files.Variables;
using DemonCastle.ProjectFiles.Exceptions;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;
using DemonCastle.ProjectFiles.Projects.Resources;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;

public partial class VariableDeclarationEditor : Control {
	private readonly ProjectResources _resources;
	private VariableDeclarationInfo? _variableDeclaration;

	public VariableDeclarationEditor(ProjectResources resources) {
		_resources = resources;
	}

	public VariableDeclarationInfo? VariableDeclaration {
		get => _variableDeclaration;
		set {
			if (value != null) Load(value);
			else Clear();
		}
	}

	public void Clear() {
		foreach (var child in GetChildren()) {
			child.QueueFree();
		}
		_variableDeclaration = null;
	}
	public void Load(VariableDeclarationInfo variableDeclaration) {
		Clear();

		var editor = GetEditor(variableDeclaration);
		AddChild(editor);
		editor.SetAnchorsAndOffsetsPreset(LayoutPreset.FullRect, margin: 5);
		_variableDeclaration = variableDeclaration;
	}

	private VariableDeclarationDetails GetEditor(VariableDeclarationInfo variableDeclaration) {
		return variableDeclaration.Type switch {
			VariableType.Boolean => new BooleanVariableDeclarationDetails((BooleanVariableDeclarationInfo)variableDeclaration),
			VariableType.Integer => new IntegerVariableDeclarationDetails((IntegerVariableDeclarationInfo)variableDeclaration),
			VariableType.Float => new FloatVariableDeclarationDetails((FloatVariableDeclarationInfo)variableDeclaration),
			VariableType.String => new StringVariableDeclarationDetails((StringVariableDeclarationInfo)variableDeclaration),
			VariableType.Monster => new MonsterVariableDeclarationDetails(_resources, (MonsterVariableDeclarationInfo)variableDeclaration),
			VariableType.Item => new ItemVariableDeclarationDetails(_resources, (ItemVariableDeclarationInfo)variableDeclaration),
			VariableType.Vector2I => new Vector2IVariableDeclarationDetails((Vector2IVariableDeclarationInfo)variableDeclaration),
			_ => throw new InvalidEnumValueException<VariableType>(variableDeclaration.Type)
		};
	}
}