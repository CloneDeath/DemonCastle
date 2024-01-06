using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations.Types;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class StringVariableDeclarationDetails : VariableDeclarationDetails {
	public StringVariableDeclarationDetails(StringVariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(StringVariableDeclarationDetails);

		AddString("Default Value", variableDeclaration, v => v.DefaultValue);
	}
}