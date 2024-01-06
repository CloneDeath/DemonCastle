using System;
using DemonCastle.Editor.Editors.Components.Properties.Reference;
using DemonCastle.Editor.Editors.Components.Properties.Vector;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;
using Godot;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class Vector2IVariableDeclarationDetails : VariableDeclarationDetails {
	public Vector2IVariableDeclarationDetails(ProjectInfo project, VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(Vector2IVariableDeclarationDetails);

		AddChild(new Vector2IProperty(new CallbackBinding<Vector2I>(
				() => (Vector2I)(variableDeclaration.DefaultValue ?? Vector2I.Zero),
				value => variableDeclaration.DefaultValue = value.ToString()),
			new Vector2IPropertyOptions {
				AllowNegative = true
			}) {
			DisplayName = "Default Value"
		});
	}
}