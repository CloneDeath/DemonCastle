using DemonCastle.Editor.Editors.Components.Properties.Vector;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class Vector2IVariableDeclarationDetails : VariableDeclarationDetails {
	public Vector2IVariableDeclarationDetails(Vector2IVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(Vector2IVariableDeclarationDetails);

		AddVector2I("Default Value", variableDeclaration, v => v.DefaultValue, new Vector2IPropertyOptions {
			AllowNegative = true
		});
	}
}