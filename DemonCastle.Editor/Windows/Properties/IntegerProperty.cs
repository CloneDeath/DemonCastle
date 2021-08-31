using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class IntegerProperty : BaseProperty {
		protected SpinBox SpinBox { get; }

		public int PropertyValue {
			get => (int)SpinBox.Value;
			set => SpinBox.Value = value;
		}
		
		public IntegerProperty() {
			Name = nameof(IntegerProperty);
			
			AddChild(SpinBox = new SpinBox {
				RectMinSize = new Vector2(20, 20),
				Editable = false,
				SizeFlagsHorizontal = (int)(SizeFlags.Fill | SizeFlags.Expand)
			});
		}
	}
}