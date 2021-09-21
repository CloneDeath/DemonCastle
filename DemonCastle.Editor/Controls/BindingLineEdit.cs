using DemonCastle.Editor.Properties;
using Godot;

namespace DemonCastle.Editor.Controls {
	public class BindingLineEdit : WrapperControl<LineEdit> {
		protected IPropertyBinding<string> PropertyBinding { get; set; }
		public IPropertyBinding<string> Binding {
			get => PropertyBinding;
			set {
				PropertyBinding = value;
				Inner.Text = PropertyBinding.Get();
			}
		}

		public string Text {
			get => Inner.Text;
			set {
				Inner.Text = value;
				OnValueChange(value);
			}
		}

		public bool Editable {
			get => Inner.Editable;
			set => Inner.Editable = value;
		}

		public BindingLineEdit() {
			Inner.Connect("text_changed", this, nameof(OnValueChange));
			RectMinSize = new Vector2(0, 24);
		}

		protected void OnValueChange(string value) {
			Binding.Set(value);
		}
	}
}