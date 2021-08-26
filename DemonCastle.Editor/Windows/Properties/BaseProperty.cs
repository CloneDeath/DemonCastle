using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public class BaseProperty : HBoxContainer {
		protected Label Label { get; }

		public string PropertyName {
			get => Label.Text;
			set => Label.Text = value;
		}
		
		public BaseProperty() {
			AddChild(Label = new Label());
		}
	}
}