using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class StringVariableDeclarationDetails : VariableDeclarationDetails {
	public StringVariableDeclarationDetails(VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(StringVariableDeclarationDetails);

		AddChild(new StringProperty(new CallbackBinding<string>(
			() => (string)(variableDeclaration.DefaultValue ?? string.Empty),
			value => variableDeclaration.DefaultValue = value)) {
			DisplayName = "Default Value"
		});
	}
}