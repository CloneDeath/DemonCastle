using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class BooleanVariableDeclarationDetails : VariableDeclarationDetails {
	public BooleanVariableDeclarationDetails(VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(BooleanVariableDeclarationDetails);

		AddChild(new BooleanProperty(new CallbackBinding<bool>(
			() => (bool)(variableDeclaration.DefaultValue ?? false),
			value => variableDeclaration.DefaultValue = value)) {
			DisplayName = "Default Value"
		});
	}
}