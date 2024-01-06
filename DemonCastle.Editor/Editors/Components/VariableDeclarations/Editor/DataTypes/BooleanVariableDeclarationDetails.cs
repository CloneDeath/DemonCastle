using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class BooleanVariableDeclarationDetails : VariableDeclarationDetails {
	public BooleanVariableDeclarationDetails(BooleanVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(BooleanVariableDeclarationDetails);

		AddBoolean("Default Value", variableDeclaration, v => v.DefaultValue);
	}
}