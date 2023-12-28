using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View.ElementTypes;

public partial class ColorRectElementView : ColorRect {
	private readonly ColorRectElementInfo _element;

	public ColorRectElementView(ColorRectElementInfo element) {
		_element = element;
		Name = nameof(ColorRectElementView);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _element.Region.Position;
		Size = _element.Region.Size;
		Color = _element.Color;
	}
}