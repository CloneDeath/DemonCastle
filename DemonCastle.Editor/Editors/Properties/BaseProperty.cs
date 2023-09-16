using Godot;

namespace DemonCastle.Editor.Windows.Properties {
	public partial class BaseProperty : HBoxContainer {
		protected Label Label { get; }

		public string DisplayName {
			get => Label.Text;
			set => Label.Text = value;
		}
		
		public BaseProperty() {
			Name = nameof(BaseProperty);
			AddChild(Label = new Label());
		}
	}
}