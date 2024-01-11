using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;

namespace DemonCastle.Editor.Editors.Scene.Elements.Types;

public partial class ColorRectElementDetails : ElementDetails {
	public ColorRectElementDetails(ColorRectElementInfo element) : base(element) {
		Name = nameof(ColorRectElementDetails);

		AddColor("Color", element, e => e.Color);
	}
}