using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class ColorProperty : BaseProperty {
		protected ColorRect ColorRect { get; }

		public Color PropertyValue {
			get => ColorRect.Color;
			set => ColorRect.Color = value;
		}
		
		public ColorProperty() {
			AddChild(ColorRect = new ColorRect {
				RectMinSize = new Vector2(16, 16)
			});
		}
	}
}
