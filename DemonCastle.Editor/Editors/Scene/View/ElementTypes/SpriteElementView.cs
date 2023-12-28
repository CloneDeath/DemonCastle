using DemonCastle.ProjectFiles.Projects.Data.Elements.Types;
using Godot;

namespace DemonCastle.Editor.Editors.Scene.View.ElementTypes;

public partial class SpriteElementView : Sprite2D {
	private readonly SpriteElementInfo _element;

	public SpriteElementView(SpriteElementInfo element) {
		_element = element;
		Name = nameof(SpriteElementView);
	}

	public override void _Process(double delta) {
		base._Process(delta);

		Position = _element.Region.Position;
		Scale = new Vector2(_element.SpriteDefinition.Region.Size.X * 1.0f/_element.Region.Size.X, 1.0f/_element.Region.Size.Y);
		Texture = _element.SpriteDefinition.Texture;
	}
}