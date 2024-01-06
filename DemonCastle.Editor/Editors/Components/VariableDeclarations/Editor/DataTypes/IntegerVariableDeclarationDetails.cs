using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class IntegerVariableDeclarationDetails : VariableDeclarationDetails {
	public IntegerVariableDeclarationDetails(VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(IntegerVariableDeclarationDetails);

		AddChild(new IntegerProperty(new CallbackBinding<int>(
			() => (int)(variableDeclaration.DefaultValue ?? 0),
			value => variableDeclaration.DefaultValue = value)) {
			DisplayName = "Default Value"
		});
	}
}