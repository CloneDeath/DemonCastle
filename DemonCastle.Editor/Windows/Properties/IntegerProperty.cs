using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class IntegerProperty : BaseProperty {
		protected SpinBox SpinBox { get; }

		public int PropertyValue {
			get => (int)SpinBox.Value;
			set => SpinBox.Value = value;
		}
		
		public IntegerProperty() {
			AddChild(SpinBox = new SpinBox {
				RectMinSize = new Vector2(200, 20)
			});
		}
	}
}