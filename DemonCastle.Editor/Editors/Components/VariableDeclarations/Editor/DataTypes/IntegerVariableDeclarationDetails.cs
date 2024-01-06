using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class IntegerVariableDeclarationDetails : VariableDeclarationDetails {
	public IntegerVariableDeclarationDetails(IntegerVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(IntegerVariableDeclarationDetails);

		AddInteger("Default Value", variableDeclaration, v => v.DefaultValue);
	}
}