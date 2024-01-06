using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Properties;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor.DataTypes;

public partial class DecimalVariableDeclarationDetails : VariableDeclarationDetails {
	public DecimalVariableDeclarationDetails(VariableDeclarationInfo variableDeclaration) : base(variableDeclaration) {
		Name = nameof(DecimalVariableDeclarationDetails);

		AddChild(new FloatProperty(new CallbackBinding<float>(
			() => (float)(variableDeclaration.DefaultValue ?? 0),
			value => variableDeclaration.DefaultValue = value)) {
			DisplayName = "Default Value"
		});
	}
}