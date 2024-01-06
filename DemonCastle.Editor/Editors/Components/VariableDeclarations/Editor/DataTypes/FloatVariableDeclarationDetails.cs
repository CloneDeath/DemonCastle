using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class FloatVariableDeclarationDetails : VariableDeclarationDetails {
	public FloatVariableDeclarationDetails(FloatVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(FloatVariableDeclarationDetails);

		AddFloat("Default Value", variableDeclaration, v => v.DefaultValue);
	}
}