using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class StringProperty : BaseProperty {
		protected LineEdit LineEdit { get; }

		public string PropertyValue {
			get => LineEdit.Text;
			set => LineEdit.Text = value;
		}
		
		public StringProperty() {
			Name = nameof(StringProperty);
			
			AddChild(LineEdit = new LineEdit {
				RectMinSize = new Vector2(20, 20),
				Editable = false,
				SizeFlagsHorizontal = (int)(SizeFlags.Fill | SizeFlags.Expand)
			});
		}
	}
}