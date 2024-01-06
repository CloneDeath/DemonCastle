using DemonCastle.Editor.Editors.Components.Properties.Rect;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;
using DemonCastle.Editor.Editors.Components.Properties;

namespace DemonCastle.Editor.Editors.Scene.Elements.Editor;

public partial class ElementDetails : PropertyCollection {
	public ElementDetails(IElementInfo element) {
		Name = nameof(ElementDetails);

		AddChild(new Label { Text = element.Type.ToString() });
		AddString("Name", element, e => e.Name);
		AddRect2I("Region", element, e => e.Region, new Rect2IPropertyOptions { AllowNegative = true });
	}
}