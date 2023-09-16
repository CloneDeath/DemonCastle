using Godot;

namespace DemonCastle.Editor.Editors.Properties; 

public partial class ColorProperty : BaseProperty {
	protected ColorRect ColorRect { get; }

	public Color PropertyValue {
		get => ColorRect.Color;
		set => ColorRect.Color = value;
	}
		
	public ColorProperty() {
		Name = nameof(ColorProperty);
			
		AddChild(ColorRect = new ColorRect {
			CustomMinimumSize = new Vector2(24, 24)
		});
	}
}