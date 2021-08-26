using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class StringProperty : BaseProperty {
		protected TextEdit TextEdit { get; }

		public string PropertyValue {
			get => TextEdit.Text;
			set => TextEdit.Text = value;
		}
		
		public StringProperty() {
			AddChild(TextEdit = new TextEdit {
				RectMinSize = new Vector2(200, 20)
			});
		}
	}
}