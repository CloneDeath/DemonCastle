using DemonCastle.Editor.Editors.Components.Properties;
using DemonCastle.Editor.Editors.Components.Properties.Rect;
using DemonCastle.ProjectFiles.Projects.Data.Elements;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.Elements;

public partial class ElementDetails : PropertyCollection {
	public ElementDetails(IElementInfo element) {
		Name = nameof(ElementDetails);

		HBoxContainer title;
		AddChild(title = new HBoxContainer());
		title.AddChild(new TextureRect { Texture = ElementTypeIcons.GetIcon(element.Type) });
		title.AddChild(new Label { Text = element.Type.ToString() });
		AddString("Name", element, e => e.Name);
		AddRect2I("Region", element, e => e.Region, new Rect2IPropertyOptions { AllowNegative = true });
	}
}