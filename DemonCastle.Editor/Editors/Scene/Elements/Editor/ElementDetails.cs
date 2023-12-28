using DemonCastle.Editor.Editors.Properties;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor;

public partial class ElementDetails : PropertyCollection {
	public ElementDetails(IElementInfo element) {
		Name = nameof(ElementDetails);

		AddChild(new Label { Text = element.Type.ToString() });
		AddString("Name", element, e => e.Name);
	}
}