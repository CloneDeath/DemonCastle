using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.ProjectFiles.Projects.Data.VariableDeclarations;

namespace DemonCastle.Editor.Editors.Components.VariableDeclarations.Editor;

public partial class VariableDeclarationDetails : PropertyCollection {
	public VariableDeclarationDetails(VariableDeclarationInfo variableDeclaration) {
		Name = nameof(VariableDeclarationDetails);

		AddString("Name", variableDeclaration, v => v.Name);
	}
}